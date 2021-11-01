using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetBattleRoutingQuery : QueryBase<BattleRouting>
    {
        public int BattleId { get; set; }

        public override async Task<BattleRouting> Execute(BeerCupStorageContext context)
        {
            var routing = await context.BattleRoutings
                                .Where(r => r.FromBattleNo == BattleId)
                                .FirstOrDefaultAsync();
            return routing;
        }
    }
}
