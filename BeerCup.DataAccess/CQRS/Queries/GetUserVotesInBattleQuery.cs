using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetUserVotesInBattleQuery : QueryBase<List<Vote>>
    {
        public int BattleId { get; set; }

        public int UserId { get; set; }

        public override async Task<List<Vote>> Execute(BeerCupStorageContext context)
        {
            return await context.Votes
                                .Where(v => v.BattleId == BattleId && v.UserId == UserId)
                                .ToListAsync();
        }
    }
}
