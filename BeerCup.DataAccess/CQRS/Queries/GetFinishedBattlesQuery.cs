using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetFinishedBattlesQuery : QueryBase<List<Battle>>
    {
        public override async Task<List<Battle>> Execute(BeerCupStorageContext context)
        {
            var response = await context.Battles.Where(b => b.ResultsPublished == true).ToListAsync();

            return response;
        }
    }
}
