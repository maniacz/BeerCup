using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Enums;
using BeerCup.Mobile.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Services.Data
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ISettingsService _settingsService;

        public AuthenticationService(IGenericRepository genericRepository, ISettingsService settingsService)
        {
            _genericRepository = genericRepository;
            _settingsService = settingsService;
        }

        public async Task<ApiResponse<User>> Authenticate(string username, string password)
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
            _settingsService.AuthTokenSetting = authToken;

            return await _genericRepository.GetAsync<User>(uri.ToString(), authToken);
        }

        public async Task<ApiResponse<User>> Register(string username, string password, string email, string accessCode)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.RegisterEndpoint
            };

            ReqistrationRequest authenticationRequest = new ReqistrationRequest
            {
                Username = username,
                Password = password,
                Email = email,
                AccessCode = accessCode
            };

            return await _genericRepository.PostAsync<ReqistrationRequest, User>(uri.ToString(), authenticationRequest);
        }

        public bool IsUserAuthenticated()
        {
            return !string.IsNullOrEmpty(_settingsService.UserNameSetting);
        }

        public UserRole UserRole => _settingsService.UserRoleSetting;

    }
}
