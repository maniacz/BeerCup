using BeerCup.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Commands
{
    public class EditLuckyVoterCommand : CommandBase<LuckyVoter, LuckyVoter>
    {
        public override async Task<LuckyVoter> Execute(BeerCupStorageContext context)
        {
            context.LuckyVoters.Update(Parameter);
            await context.SaveChangesAsync();
            return Parameter;
        }
    }
}
