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
    public class BreweryDataService : IBreweryDataService
    {
        private readonly IGenericRepository _genericRepository;

        public BreweryDataService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<Brewery>> GetAllNotAssignedInFirstRoundBreweries()
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BreweriesEndpoint + "/NotAssigned"
            };

            var response = await _genericRepository.GetAsync<ApiResponse<List<Brewery>>>(uri.ToString());

            return response?.Data;
        }
    }
}
