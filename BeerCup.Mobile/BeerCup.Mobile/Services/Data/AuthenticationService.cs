using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Services.Data
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository _genericRepository;

        public AuthenticationService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public bool IsUserAuthenticated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<AuthenticationResponse> Authenticate(string username, string password)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AuthenticateEndpoint
            };

            //AuthenticationRequest request = new AuthenticationRequest()
            //{
            //    Username = username,
            //    Password = password
            //};

            var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

            return await _genericRepository.GetAsync<AuthenticationResponse>(uri.ToString(), authToken);
        }

        public Task<AuthenticationResponse> Register(string username, string password, string email)
        {
            throw new NotImplementedException();
        }
    }
}
