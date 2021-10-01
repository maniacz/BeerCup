using BeerCup.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Commands
{
    public class RemoveVoteCommand : CommandBase<Vote, Vote>
    {
        public override async Task<Vote> Execute(BeerCupStorageContext context)
        {
            context.Votes.Remove(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
