using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Models
{
    public class AuthenticationRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
