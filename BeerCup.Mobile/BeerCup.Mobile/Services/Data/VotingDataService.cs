using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Services.Data
{
    public class VotingDataService : IVotingDataService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ISettingsService _settingsService;

        public VotingDataService(IGenericRepository genericRepository, ISettingsService settingsService)
        {
            _genericRepository = genericRepository;
            _settingsService = settingsService;
        }

        public async Task<IEnumerable<Vote>> SendVotes(IEnumerable<Beer> chosenBeers)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BattlesEndpoint
            };

            var userId = _settingsService.UserIdSetting;

            List<Vote> userVotes = new List<Vote>();
            foreach (var beer in chosenBeers)
            {
                var vote = new Vote { VoterId = userId, VotedBeer = beer };
                userVotes.Add(vote);
                await _genericRepository.PostAsync<Vote>(uri.ToString() , vote);
            }

            return userVotes;
        }
    }
}
