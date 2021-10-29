using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeerCup.Mobile.Services.Data
{
    public class AdminPanelDataService : IAdminPanelDataService
    {
        private readonly IGenericRepository _genericRepository;

        public AdminPanelDataService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<Battle> StartBattle(BattlePlace battlePlace)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint
            };

            var battle = new Battle
            {
                Place = battlePlace
            };

            return await _genericRepository.PutAsync(uri.ToString(), battle);
        }

        public async Task<Battle> EndBattle(Battle battle)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/EndBattle"
            };

            return await _genericRepository.PutAsync(uri.ToString(), battle);
        }

        public async Task<Battle> PublishResults(Battle battle)
        {
            battle.ResultsPublished = true;
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/PublishResults"
            };

            return await _genericRepository.PutAsync(uri.ToString(), battle);
        }

        public async Task<Battle> HideResults(Battle battle)
        {
            battle.ResultsPublished = false;
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/PublishResults"
            };

            return await _genericRepository.PutAsync(uri.ToString(), battle);
        }
    }
}
