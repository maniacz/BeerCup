using BeerCup.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Commands
{
    public class StartBattleCommand : CommandBase<Battle, Battle>
    {
        public async override Task<Battle> Execute(BeerCupStorageContext context)
        {
            context.Battles.Update(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
