using BeerCup.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Commands
{
    public class RemoveBreweryCommand : CommandBase<Brewery, Brewery>
    {
        public override async Task<Brewery> Execute(BeerCupStorageContext context)
        {
            context.Breweries.Remove(context.Breweries.Where(b => b.Id == Parameter.Id).SingleOrDefault());
            await context.SaveChangesAsync();
            return Parameter;
        }
    }
}
