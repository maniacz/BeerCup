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
    public class AdminPanelDataService : IAdminPanelDataService
    {
        private readonly IGenericRepository _genericRepository;

        public AdminPanelDataService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<Battle> StartBattle()
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint
            };

            return await _genericRepository.PutAsync<Battle>(uri.ToString(), null);
        }
    }
}
