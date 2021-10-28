using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetTodaysBattleQuery : QueryBase<Battle>
    {
        public async override Task<Battle> Execute(BeerCupStorageContext context)
        {
            var battle = await context.Battles
                                .Where(b => b.Date.Value.Date == DateTime.Today)
                                .SingleOrDefaultAsync();
            return battle;
        }
    }
}
