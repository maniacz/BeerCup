using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetBattleByNoQuery : QueryBase<Battle>
    {
        public int BattleNo { get; set; }

        public override async Task<Battle> Execute(BeerCupStorageContext context)
        {
            var battle = await context.Battles
                                .Include(b => b.Beers)
                                .ThenInclude(b => b.Brewery)
                                .FirstOrDefaultAsync(b => b.BattleNo == this.BattleNo);
            return battle;
        }
    }
}
