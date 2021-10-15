using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class CheckAccessCodeNotUsedQuery : QueryBase<User>
    {
        public int AccessCodeId { get; set; }

        public override async Task<User> Execute(BeerCupStorageContext context)
        {
            var userWithAssignedAccessCode = await context.Users
                                            .Where(u => u.AccessCodeId == AccessCodeId)
                                            .SingleOrDefaultAsync();

            return userWithAssignedAccessCode;
        }
    }
}
