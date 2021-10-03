﻿using BeerCup.Mobile.Enums;
using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Contracts.Services.Data
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Register(string username, string password, string email, string accessCode);

        Task<AuthenticationResponse> Authenticate(string username, string password);

        bool IsUserAuthenticated();

        UserRole UserRole { get; }
    }
}
