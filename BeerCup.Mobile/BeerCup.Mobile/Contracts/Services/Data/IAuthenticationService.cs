using BeerCup.Mobile.Enums;
using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Contracts.Services.Data
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<User>> Register(string username, string password, string email, string accessCode);

        Task<ApiResponse<User>> Authenticate(string username, string password);

        bool IsUserAuthenticated();

        UserRole UserRole { get; }
    }
}
