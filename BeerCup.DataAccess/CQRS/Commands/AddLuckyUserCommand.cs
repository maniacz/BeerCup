using BeerCup.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Commands
{
    public class AddLuckyUserCommand : CommandBase<LuckyVoter, LuckyVoter>
    {

        public override async Task<LuckyVoter> Execute(BeerCupStorageContext context)
        {
            await context.LuckyVoters.AddAsync(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
