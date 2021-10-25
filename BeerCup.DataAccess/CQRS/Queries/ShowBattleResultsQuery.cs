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
            var results = context.Breweries.SelectMany(b => context.Votes
                                        .Where(v => v.BattleId == BattleId && v.BreweryId == b.Id).DefaultIfEmpty(), (b, v) => new
                                        {
                                            Brewery = b,
                                            Vote = v
                                        }).AsEnumerable()
                                            .GroupBy(a => a.Brewery)
                                                .Select(g => new BattleResult
                                                {
                                                    Brewery = g.Key,
                                                    VotesReceived = g.Where(a => a.Vote != null).Count()
                                                })
                                            .OrderByDescending(g => g.VotesReceived).ThenBy(g => g.Brewery.Name)
                                            .ToList();

            return Task.FromResult(results);
        }
    }
}
