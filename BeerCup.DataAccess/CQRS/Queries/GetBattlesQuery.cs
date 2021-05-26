using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetBattlesQuery : QueryBase<List<Battle>>
    {
        public override Task<List<Battle>> Execute(BeerCupStorageContext context)
        {
            return context.Battles.ToListAsync();
        }
    }
}
