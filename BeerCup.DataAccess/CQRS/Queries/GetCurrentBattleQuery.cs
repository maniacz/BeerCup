using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetCurrentBattleQuery : QueryBase<Battle>
    {
        public async override Task<Battle> Execute(BeerCupStorageContext context)
        {
            var currentBattle = await context.Battles
                                        .Where(b => b.IsRunning == true)
                                        .Include(b => b.Place)
                                        .SingleOrDefaultAsync();
            return currentBattle;
        }
    }
}
