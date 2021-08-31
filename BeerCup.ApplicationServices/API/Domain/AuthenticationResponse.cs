using BeerCup.ApplicationServices.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class AuthenticationResponse : ResponseBase<User>
    {
        public bool IsAuthenticated { get; set; }
    }
}
