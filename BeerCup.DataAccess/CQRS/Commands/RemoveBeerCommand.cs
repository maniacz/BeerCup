using BeerCup.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Commands
{
    public class RemoveBeerCommand : CommandBase<Beer, Beer>
    {
        public override async Task<Beer> Execute(BeerCupStorageContext context)
        {
            context.Beers.Remove(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
