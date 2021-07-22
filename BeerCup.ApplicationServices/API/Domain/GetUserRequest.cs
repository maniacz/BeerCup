using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class GetUserRequest : RequestBase, IRequest<GetUserResponse>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
