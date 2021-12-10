using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Services.Data
{
    public class BeerDataService : IBeerDataService
    {
        private readonly IGenericRepository _genericRepository;

        public BeerDataService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<Beer> RegisterBeerInFirstRound(int battleId, int breweryId)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BeersEndpoint
            };

            var beerToRegister = new Beer
            {
                BattleId = battleId,
                BreweryId = breweryId
            };

            var response = await _genericRepository.PostAsync<Beer, ApiResponse<Beer>>(uri.ToString(), beerToRegister);

            return response?.Data;
        }
    }
}
