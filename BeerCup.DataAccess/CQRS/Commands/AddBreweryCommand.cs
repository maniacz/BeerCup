using BeerCup.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Commands
{
    public class AddBreweryCommand : CommandBase<Brewery, Brewery>
    {
        public async override Task<Brewery> Execute(BeerCupStorageContext context)
        {
            await context.Breweries.AddAsync(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
