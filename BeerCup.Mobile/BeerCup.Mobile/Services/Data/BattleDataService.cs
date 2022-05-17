using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Services.Data
{
    public class BattleDataService : IBattleDataService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;
        private readonly string _authToken;

        public BattleDataService(IGenericRepository genericRepository, ISettingsService settingsService, INavigationService navigationService)
        {
            _genericRepository = genericRepository;
            _settingsService = settingsService;
            _navigationService = navigationService;
            _authToken = settingsService.AuthTokenSetting;
        }

        public async Task<Battle> GetCurrentRunningBattle()
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BattlesEndpoint + "/current"
            };

            var response = await _genericRepository.GetAsync<Battle>(uri.ToString());

            return response?.Data;
        }

        public async Task<List<Result>> GetBattleResults(int battleId)
        {
            var battleResults = new List<Result>();

            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/ShowResults/" + battleId
            };

            var response = await _genericRepository.GetAsync<List<Result>>(uri.ToString());

            int rank = 1;
            int allBattleVotes = 0;
            foreach (var result in response.Data.OrderByDescending(r => r.VotesReceived).ThenBy(r => r.Brewery.Name))
            {
                result.FinalRank = rank++;
                allBattleVotes += result.VotesReceived;
                battleResults.Add(result);
            }

            foreach (var result in battleResults)
            {
                var precentage = (decimal)result.VotesReceived / allBattleVotes * 100;
                result.Precentage = precentage;
            }

            return battleResults;
        }

        public async Task<Battle> GetTodaysBattle()
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BattlesEndpoint + "/TodaysBattle"
            };

            var response = await _genericRepository.GetAsync<Battle>(uri.ToString());

            if (!string.IsNullOrEmpty(response.Error))
            {
                switch (response.Error)
                {
                    case ApiErrorResponseConstants.Unauthorized:
                        await _navigationService.NavigateBackAsync();
                        return null;
                    default:
                        await _navigationService.NavigateBackAsync();
                        break;
                }
            }

            return response?.Data;
        }

        public async Task<List<Battle>> GetFinishedBattles()
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BattlesEndpoint + "/Finished"
            };

            var response = await _genericRepository.GetAsync<List<Battle>>(uri.ToString());

            return response?.Data;
        }

        public async Task<List<Vote>> GetBattleUserVotes(int battleId)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BattlesEndpoint + "/" + battleId + "/" + _settingsService.UserIdSetting
            };

            var response = await _genericRepository.GetAsync<List<Vote>>(uri.ToString());

            return response?.Data;
        }

        public async Task<List<Battle>> GetAllBattles()
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BattlesEndpoint
            };

            var response = await _genericRepository.GetAsync<List<Battle>>(uri.ToString(), _authToken);

            return response?.Data;
        }

        public async Task<List<Brewery>> GetBreweriesFromBattle(int battleNo)
        {
            var battle = await GetBattleByBattleNo(battleNo);

            if (battle != null)
            {
                var breweries = battle.Beers.Select(b => b.BrewedBy).OrderBy(b => b.Name).ToList();
                return breweries;
            }

            return null;
        }

        public async Task<Battle> GetBattleByBattleNo(int battleNo)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BattlesEndpoint + "/" + battleNo
            };

            var response = await _genericRepository.GetAsync<Battle>(uri.ToString(), _authToken);

            return response?.Data;
        }

        public async Task<List<Battle>> GetFirstRoundBattles()
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BattlesEndpoint + "/firstRound"
            };

            var response = await _genericRepository.GetAsync<List<Battle>>(uri.ToString());

            return response?.Data;
        }
    }
}
