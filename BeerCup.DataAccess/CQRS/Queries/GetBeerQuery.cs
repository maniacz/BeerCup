using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetBeerQuery : QueryBase<Beer>
    {
        public int Id { get; set; }

        public override Task<Beer> Execute(BeerCupStorageContext context)
        {
            return context.Beers.FirstOrDefaultAsync(b => b.Id == Id);
        }
    }
}
