using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetFirstBattleFromRoutingQuery : QueryBase<Battle>
    {
        public int NextBattleNo { get; set; }

        public override async Task<Battle> Execute(BeerCupStorageContext context)
        {
            var firstBattleNo = await context.BattleRoutings
                                    .Where(r => r.ToBattleNo == NextBattleNo && r.IsSecondBattle == false)
                                    .Select(r => r.FromBattleNo)
                                    .SingleOrDefaultAsync();

            var firstBattle = await context.Battles
                                    .Where(b => b.BattleNo == firstBattleNo)
                                    .SingleOrDefaultAsync();
            return firstBattle;
        }
    }
}
