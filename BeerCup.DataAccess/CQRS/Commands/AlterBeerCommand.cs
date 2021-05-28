using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Commands
{
    public class AlterBeerCommand : CommandBase<Beer, Beer>
    {
        public int Id { get; set; }

        public async override Task<Beer> Execute(BeerCupStorageContext context)
        {
            context.Beers.Update(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
