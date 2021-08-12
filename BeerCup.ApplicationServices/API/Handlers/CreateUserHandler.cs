using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
using BeerCup.ApplicationServices.API.ErrorHandling;
using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Commands;
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
        private readonly IMapper mapper;
        private readonly ILogger<CreateUserHandler> logger;

        public CreateUserHandler(ICommandExecutor commandExecutor, IMapper mapper, ILogger<CreateUserHandler> logger)
        {
            this.commandExecutor = commandExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var salt = Encryption.Encryption.GenerateSalt();
            var hashedPassword = Encryption.Encryption.HashPassword(request.Password, salt);

            var user = mapper.Map<DataAccess.Entities.User>(request);
            user.Salt = Convert.ToBase64String(salt);
            user.Password = hashedPassword;

            var command = new AddUserCommand()
            {
                Parameter = user
            };

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
    }
}
