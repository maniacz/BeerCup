using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetBreweriesQuery : QueryBase<List<Brewery>>
    {
        public override async Task<List<Brewery>> Execute(BeerCupStorageContext context)
        {
            return await context.Breweries.ToListAsync();
        }
    }
}
