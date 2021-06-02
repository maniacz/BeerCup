using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetBeersQuery : QueryBase<List<Beer>>
    {
        public int BattleId { get; set; }

        public override Task<List<Beer>> Execute(BeerCupStorageContext context)
        {
            if (BattleId != 0)
            {
                return context.Beers.Where(b => b.BattleId == BattleId).ToListAsync();
            }

            return context.Beers.ToListAsync();
        }
    }
}
