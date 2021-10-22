using BeerCup.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Commands
{
    public class SetBattlePlaceCommand : CommandBase<BattlePlace, BattlePlace>
    {
        public override async Task<BattlePlace> Execute(BeerCupStorageContext context)
        {
            await context.BattlePlaces.AddAsync(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
