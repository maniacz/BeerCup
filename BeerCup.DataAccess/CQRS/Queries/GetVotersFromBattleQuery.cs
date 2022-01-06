using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetVotersFromBattleQuery : QueryBase<List<User>>
    {
        public int BattleId { get; set; }

        public override async Task<List<User>> Execute(BeerCupStorageContext context)
        {
            var votersIds = await context.Votes.Where(v => v.BattleId == BattleId).GroupBy(g => g.UserId).Select(v => v.Key).ToListAsync();
            var voters = await context.Users.Where(u => votersIds.Contains(u.Id)).ToListAsync();

            return voters;
        }
    }
}
