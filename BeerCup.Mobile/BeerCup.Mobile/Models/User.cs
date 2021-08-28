using BeerCup.Mobile.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Models
{
    public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        //todo: Tu może trza osobno se enuma w projekcie zrobić (low coupling)
        public UserRole Role { get; set; }
    }
}
