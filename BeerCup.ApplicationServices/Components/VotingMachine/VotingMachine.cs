using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.Components.VotingMachine
{
    public class VotingMachine : IVotingMachine
    {
        private readonly IQueryExecutor _queryExecutor;

        public VotingMachine(IQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;
        }

        private async Task<int> GetMobileVotesCount(int battleId)
        {
            var query = new GetVotersFromBattleQuery();
            query.BattleId = battleId;
            var results = await _queryExecutor.Execute(query);

            return results?.Count ?? 0;
        }

        public async Task<bool> IsDrawWinnerFromPaperVotes(int paperVotesCount, int battleId)
        {
            var mobileVotesCount = await GetMobileVotesCount(battleId);
            var allVotesCount = paperVotesCount + mobileVotesCount;
            var random = new Random();
            var result = random.Next(allVotesCount);
            if (result < paperVotesCount)
            {
                return true;
            }

            return false;
        }
    }
}
