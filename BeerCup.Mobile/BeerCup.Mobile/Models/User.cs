using BeerCup.Mobile.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }

        public bool IsAuthenticated { get; set; }
    }
}
