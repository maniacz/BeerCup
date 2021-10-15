using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetAccessCodeIdQuery : QueryBase<int>
    {
        public string AccessCode { get; set; }

        public override async Task<int> Execute(BeerCupStorageContext context)
        {
            var accessCodeId = await context.AccessCodes
                            .Where(c => c.Code == AccessCode)
                            .Select(c => c.AccessCodeId)
                            .SingleOrDefaultAsync();

            return accessCodeId;
        }
    }
}
