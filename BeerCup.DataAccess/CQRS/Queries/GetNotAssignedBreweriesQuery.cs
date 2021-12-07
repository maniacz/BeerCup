using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetNotAssignedBreweriesQuery : QueryBase<List<Brewery>>
    {
        public override async Task<List<Brewery>> Execute(BeerCupStorageContext context)
        {
            var firstRoundBattlesQuery = new GetFirstRoundBattlesQuery();
            List<Battle> firstRoundBattles = await firstRoundBattlesQuery.Execute(context);

            var lastFirstRoundBattleId = firstRoundBattles.OrderByDescending(b => b.Id).Select(b => b.Id).FirstOrDefault();

            var breweries = await context.Breweries.Where(b => !b.Beers.Any(beer => beer.BreweryId == b.Id && beer.BattleId < lastFirstRoundBattleId)).ToListAsync();

            return breweries;
        }
    }
}
