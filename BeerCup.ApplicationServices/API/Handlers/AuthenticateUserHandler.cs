using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
using BeerCup.ApplicationServices.API.Domain.Models.DTO;
using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Handlers
{
    public class AuthenticateUserHandler : IRequestHandler<AuthenticationRequest, AuthenticationResponse>
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticateUserHandler> _logger;

        public AuthenticateUserHandler(IQueryExecutor queryExecutor, IMapper mapper, ILogger<AuthenticateUserHandler> logger)
        {
            _queryExecutor = queryExecutor;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AuthenticationResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var getUserQuery = new GetUserQuery()
            {
                Username = request.Username
            };

            var userFromDb = await _queryExecutor.Execute(getUserQuery);
            var userSalt = Convert.FromBase64String(userFromDb.Salt);
            var hashedPassword = Encryption.Encryption.HashPassword(request.Password, userSalt);

            var authenticateUserQuery = new AuthenticateUserQuery()
            {
                Username = request.Username,
                Password = hashedPassword
            };

            var authenticationResult = await _queryExecutor.Execute(authenticateUserQuery);

            if (authenticationResult == null)
            {
                return new AuthenticationResponse()
                {
                    IsAuthenticated = false
                };
            }

            return new AuthenticationResponse()
            {
                IsAuthenticated = true,
                Data = _mapper.Map<UserDTO>(userFromDb)
            };
        }
    }
}
