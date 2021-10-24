using AutoMapper;
using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.Models.DTO;
using BeerCup.Mobile.Models.DTO.Responses;
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
        private readonly IMapper _mapper;

        public AdminPanelDataService(IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
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

            var battleFromDb = await _genericRepository.PutAsync<Battle, BattleResponse>(uri.ToString(), battle);
            return _mapper.Map<Battle>(battleFromDb);
        }

        public async Task<Battle> EndBattle(Battle battle)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/EndBattle"
            };

            var battleFromDb = await _genericRepository.PutAsync<Battle, BattleResponse>(uri.ToString(), battle);
            return _mapper.Map<Battle>(battleFromDb);
        }
    }
}
