using BeerCup.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Commands
{
    public class RegisterVoteCommand : CommandBase<Vote, Vote>
    {
        public override async Task<Vote> Execute(BeerCupStorageContext context)
        {
            await context.Votes.AddAsync(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
