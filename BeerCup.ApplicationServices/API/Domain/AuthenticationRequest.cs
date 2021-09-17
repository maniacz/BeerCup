using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class AuthenticationRequest : IRequest<AuthenticationResponse>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        //todo: potem tu na potrzeby rejestracji dodać resztę typu e-mail itd
    }
}
