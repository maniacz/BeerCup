using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetUserQuery : QueryBase<User>
    {
        public string Username { get; set; }

        public override Task<User> Execute(BeerCupStorageContext context)
        {
            return context.Users.FirstOrDefaultAsync(u => u.Username == this.Username);
        }
    }
}
