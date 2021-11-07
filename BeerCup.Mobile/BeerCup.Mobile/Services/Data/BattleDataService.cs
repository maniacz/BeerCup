using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Services.Data
{
    public class BattleDataService : IBattleDataService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ISettingsService _settingsService;
        private readonly string _authToken;

        public BattleDataService(IGenericRepository genericRepository, ISettingsService settingsService)
        {
            _genericRepository = genericRepository;
            _settingsService = settingsService;
            _authToken = settingsService.AuthTokenSetting;
        }

        public async Task<Battle> GetCurrentRunningBattle()
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BattlesEndpoint + "/current"
            };

            var response = await _genericRepository.GetAsync<ApiResponse<Battle>>(uri.ToString());

            return response?.Data;
        }

        public async Task<List<Result>> GetBattleResults(int battleId)
        {
            var battleResults = new List<Result>();

            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/ShowResults/" + battleId
            };

            var response = await _genericRepository.GetAsync<ApiResponse<List<Result>>>(uri.ToString());

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

            var response = await _genericRepository.GetAsync<ApiResponse<Battle>>(uri.ToString());
            if (response.Error != null)
            {
                return null;
            }

            return response.Data;
        }

        public async Task<List<Battle>> GetFinishedBattles()
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BattlesEndpoint + "/Finished"
            };

            var response = await _genericRepository.GetAsync<ApiResponse<List<Battle>>>(uri.ToString());

            return response?.Data;
        }

        public async Task<List<Vote>> GetBattleUserVotes(int battleId)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BattlesEndpoint + "/" + battleId + "/" + _settingsService.UserIdSetting
            };

            var response = await _genericRepository.GetAsync<ApiResponse<List<Vote>>>(uri.ToString());

            return response?.Data;
        }

        public async Task<List<Battle>> GetAllBattles()
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BattlesEndpoint
            };

            var response = await _genericRepository.GetAsync<ApiResponse<List<Battle>>>(uri.ToString(), _authToken);

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

            var response = await _genericRepository.GetAsync<ApiResponse<Battle>>(uri.ToString(), _authToken);

            return response?.Data;
        }
    }
}
