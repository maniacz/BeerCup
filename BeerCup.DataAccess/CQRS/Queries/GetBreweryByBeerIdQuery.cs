using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetBreweryByBeerIdQuery : QueryBase<Brewery>
    {
        public int BeerId { get; set; }

        public override async Task<Brewery> Execute(BeerCupStorageContext context)
        {
            var brewery = await context.Breweries
                                .Where(b => b.Beers.Any(b => b.Id == BeerId))
                                .FirstOrDefaultAsync();

            return brewery;
        }
    }
}
