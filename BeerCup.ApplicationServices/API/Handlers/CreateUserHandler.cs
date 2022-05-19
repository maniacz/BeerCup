using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models.DAO;
using BeerCup.ApplicationServices.API.Domain.Models.DTO;
using BeerCup.ApplicationServices.API.ErrorHandling;
using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Commands;
using BeerCup.DataAccess.CQRS.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserHandler> logger;

        public CreateUserHandler(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor, IMapper mapper, ILogger<CreateUserHandler> logger)
        {
            this.commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
            this._mapper = mapper;
            this.logger = logger;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var accessCodeId = await ConfirmAccessCodeIsValid(request.AccessCode);
            if (accessCodeId == 0)
            {
                var response =
                 new CreateUserResponse
                 {
                     Data = new UserDTO { IsAuthenticated = false },
                     Error = new ErrorModel(ErrorType.NotValidAccessCode)
                 };

                return response;
            }

            var salt = Encryption.Encryption.GenerateSalt();
            var hashedPassword = Encryption.Encryption.HashPassword(request.Password, salt);

            var userDAO = _mapper.Map<UserDAO>(request);
            userDAO.AccessCodeId = accessCodeId;

            var user = _mapper.Map<DataAccess.Entities.User>(userDAO);
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
                        Data = new UserDTO { IsAuthenticated = false },
                        Error = new ErrorModel(ErrorType.UserAlreadyExists)
                    };
                }
            }

            if (userFromDb == null)
            {
                return new CreateUserResponse()
                {
                    Data = new UserDTO { IsAuthenticated = false },
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }

            var mappedUser = _mapper.Map<UserDTO>(userFromDb);
            mappedUser.IsAuthenticated = true;

            return new CreateUserResponse()
            {
                Data = mappedUser
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