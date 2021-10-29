using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Services.Data
{
    public class BattleDataService : IBattleDataService
    {
        private readonly IGenericRepository _genericRepository;

        public BattleDataService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
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
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/ShowResults/" + battleId
            };

            var response = await _genericRepository.GetAsync<ApiResponse<List<Result>>>(uri.ToString());

            return response.Data;
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
    }
}
