using BeerCup.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Commands
{
    public class PublishBattleResultsCommand : CommandBase<Battle, Battle>
    {
        public override async Task<Battle> Execute(BeerCupStorageContext context)
        {
            Parameter.ResultsPublished = true;
            context.Battles.Update(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
