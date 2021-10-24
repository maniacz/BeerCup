using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetRunningBattleQuery : QueryBase<Battle>
    {
        public int BattleId { get; set; }

        public override async Task<Battle> Execute(BeerCupStorageContext context)
        {
            return await context.Battles.Where(b => b.IsRunning && b.Id == BattleId).SingleOrDefaultAsync();
        }
    }
}
