using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
using BeerCup.ApplicationServices.API.Domain.Models.DAO;
using BeerCup.ApplicationServices.API.Domain.Models.DTO;
using BeerCup.ApplicationServices.API.ErrorHandling;
using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Commands;
using BeerCup.DataAccess.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<CreateUserHandler> logger;

        public CreateUserHandler(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor, IMapper mapper, ILogger<CreateUserHandler> logger)
        {
            this.commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var accessCodeId = await ConfirmAccessCodeIsValid(request.AccessCode);
            if (accessCodeId == 0)
            {
                return new CreateUserResponse
                {
                    Error = new ErrorModel(ErrorType.NotValidAccessCode)
                };
            }

            var salt = Encryption.Encryption.GenerateSalt();
            var hashedPassword = Encryption.Encryption.HashPassword(request.Password, salt);

            var userDAO = mapper.Map<UserDAO>(request);
            userDAO.AccessCodeId = accessCodeId;

            var user = mapper.Map<DataAccess.Entities.User>(userDAO);
            user.Salt = Convert.ToBase64String(salt);
            user.Password = hashedPassword;

            var command = new AddUserCommand()
            {
                Parameter = user
            };

            switch (request.AccessCode)
            {
                case string s when s.StartsWith("A"):
                    command.Parameter.Role = DataAccess.Enums.UserRole.Admin;
                    break;
                case string s when s.StartsWith("V"):
                    command.Parameter.Role = DataAccess.Enums.UserRole.Voter;
                    break;
                case string s when s.StartsWith("B"):
                    command.Parameter.Role = DataAccess.Enums.UserRole.BreweryOwner;
                    break;
                default:
                    break;
            }

            DataAccess.Entities.User userFromDb = null;
            try
            {
                userFromDb = await commandExecutor.Execute(command);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.StartsWith("Cannot insert duplicate key row"))
                {
                    return new CreateUserResponse
                    {
                        Error = new ErrorModel(ErrorType.UserAlreadyExists)
                    };
                }
            }

            if (userFromDb == null)
            {
                return new CreateUserResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }

            return new CreateUserResponse()
            {
                IsAuthenticated = true,
                Data = mapper.Map<UserDTO>(userFromDb)
            };
        }

        private async Task<int> ConfirmAccessCodeIsValid(string accessCode)
        {
            var query = new GetAccessCodeIdQuery
            {
                AccessCode = accessCode
            };

            int accessCodeId = await _queryExecutor.Execute(query);

            var checkQuery = new CheckAccessCodeNotUsedQuery
            {
                AccessCodeId = accessCodeId
            };

            var checkResult = await _queryExecutor.Execute(checkQuery);

            if (checkResult != null)
            {
                //Istnieje już zarejestrowany user używający tego access code
                return 0;
            }

            return accessCodeId;
        }
    }
}