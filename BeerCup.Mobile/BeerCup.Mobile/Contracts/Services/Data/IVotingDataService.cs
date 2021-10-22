using BeerCup.Mobile.Models;
using BeerCup.Mobile.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Contracts.Services.Data
{
    public interface IVotingDataService
    {
        Task<IEnumerable<VoteResponse>> SendVotes(IEnumerable<Beer> votes);
    }
}
