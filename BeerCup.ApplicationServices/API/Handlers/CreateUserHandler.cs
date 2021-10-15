using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
using BeerCup.ApplicationServices.API.Domain.Models.DTO;
using BeerCup.ApplicationServices.API.ErrorHandling;
using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Commands;
using BeerCup.DataAccess.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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

            var userDTO = mapper.Map<UserDTO>(request);
            userDTO.AccessCodeId = accessCodeId;

            var user = mapper.Map<DataAccess.Entities.User>(userDTO);
            user.Salt = Convert.ToBase64String(salt);
            user.Password = hashedPassword;

            var command = new AddUserCommand()
            {
                Parameter = user
            };

            //todo: ulepsz weryfikację czy ktoś ma dostać role admina
            if (request.AccessCode.StartsWith("A"))
            {
                command.Parameter.Role = DataAccess.Enums.UserRole.Admin;
            }

            var userFromDb = await commandExecutor.Execute(command);
            if (userFromDb == null)
            {
                return new CreateUserResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }

            return new CreateUserResponse()
            {
                Data = mapper.Map<User>(userFromDb)
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