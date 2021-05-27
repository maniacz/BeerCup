using BeerCup.DataAccess.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly BeerCupStorageContext context;

        public CommandExecutor(BeerCupStorageContext context)
        {
            this.context = context;
        }

        public Task<TResult> Execute<TParameters, TResult>(CommandBase<TParameters, TResult> command)
        {
            return command.Execute(this.context);
        }
    }
}
