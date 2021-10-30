using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class ShowBattleResultsQuery : QueryBase<List<BattleResult>>
    {
        public int BattleId { get; set; }

        public override Task<List<BattleResult>> Execute(BeerCupStorageContext context)
        {
            var results = (from b in context.Set<Brewery>()
                           join p in context.Set<Beer>()
                             on b.Id equals p.BreweryId into grouping
                           from p in grouping.DefaultIfEmpty()
                           where p.BattleId == BattleId
                           orderby p.Votes.Count descending, b.Name ascending
                           select new BattleResult
                           {
                               Brewery = b,
                               VotesReceived = p.Votes.Count,
                               BeerNo = p.NumberInBattle,
                               BeerId = p.Id
                           }).ToList();

            return Task.FromResult(results);
        }
    }
}
