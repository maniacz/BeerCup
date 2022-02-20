using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.Components.VotingMachine
{
    public interface IVotingMachine
    {
        Task<bool> IsDrawWinnerFromPaperVotes(int paperVotesCount, int battleId);
    }
}
