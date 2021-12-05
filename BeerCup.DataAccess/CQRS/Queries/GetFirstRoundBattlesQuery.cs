using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetFirstRoundBattlesQuery : QueryBase<List<Battle>>
    {
        public override async Task<List<Battle>> Execute(BeerCupStorageContext context)
        {
            return await context.Battles.Where(b => b.BattleNo < 9).OrderBy(b => b.BattleNo).ToListAsync();
        }
    }
}
