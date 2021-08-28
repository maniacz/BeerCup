using BeerCup.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Contracts.Services.Data
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Register(string username, string password, string email);

        Task<AuthenticationResponse> Authenticate(string username, string password);

        bool IsUserAuthenticated { get; set; }
    }
}
