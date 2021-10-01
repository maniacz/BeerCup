using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetBeerInBattleQuery : QueryBase<Beer>
    {
        public int BattleId { get; set; }
        public int BeerNumber { get; set; }

        public override async Task<Beer> Execute(BeerCupStorageContext context)
        {
            return await context.Beers
                                .Where(b => b.BattleId == BattleId && b.NumberInBattle == BeerNumber)
                                .SingleOrDefaultAsync();
        }
    }
}
