using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetBattleQuery : QueryBase<Battle>
    {
        public int Id { get; set; }

        public override async Task<Battle> Execute(BeerCupStorageContext context)
        {
            var battle = await context.Battles.FirstOrDefaultAsync(b => b.Id == this.Id);
            return battle;
        }
    }
}
