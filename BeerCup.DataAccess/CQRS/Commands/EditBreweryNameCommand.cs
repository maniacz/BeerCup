using BeerCup.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Commands
{
    public class EditBreweryNameCommand : CommandBase<Brewery, Brewery>
    {
        public override async Task<Brewery> Execute(BeerCupStorageContext context)
        {
            context.Breweries.Update(Parameter);
            await context.SaveChangesAsync();
            return Parameter;
        }
    }
}
