using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class AuthenticateUserQuery : QueryBase<User>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public override Task<User> Execute(BeerCupStorageContext context)
        {
            return context.Users.SingleOrDefaultAsync(u => u.Username == this.Username && u.Password == this.Password);
        }
    }
}
