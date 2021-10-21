using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Models
{
    public class AuthenticationResponse
    {
        public bool IsAuthenticated { get; set; }

        public User Data { get; set; }

        public string Error { get; set; }
    }
}
