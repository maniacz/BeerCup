using AutoMapper;
using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Services.Data
{
    public class BattleDataService : IBattleDataService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        public BattleDataService(IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Battle> GetCurrentRunningBattle()
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BattlesEndpoint + "/current"
            };

            var response = await _genericRepository.GetAsync<BattleResponseDTO>(uri.ToString());
            if (response.Data == null)
            {
                return null;
            }

            var modelBattle = _mapper.Map<Battle>(response);
            return modelBattle;
        }
    }
}
