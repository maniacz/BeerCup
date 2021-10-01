using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetVotesQuery : QueryBase<List<Vote>>
    {
        public int UserId { get; set; }

        public int BattleId { get; set; }

        public override async Task<List<Vote>> Execute(BeerCupStorageContext context)
        {
            var votes = await context.Votes
                                .Where(v => v.UserId == UserId && v.BattleId == BattleId)
                                .ToListAsync();

            return votes;
        }
    }
}
