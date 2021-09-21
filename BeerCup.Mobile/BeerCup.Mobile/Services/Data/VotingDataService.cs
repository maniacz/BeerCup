using AutoMapper;
using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BeerCup.Mobile.Services.Data
{
    public class VotingDataService : IVotingDataService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ISettingsService _settingsService;
        private readonly IMapper _mapper;

        public VotingDataService(IGenericRepository genericRepository, ISettingsService settingsService, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _settingsService = settingsService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Vote>> SendVotes(IEnumerable<Beer> chosenBeers)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BattlesEndpoint
            };

            var userId = _settingsService.UserIdSetting;

            List<Vote> userVotes = new List<Vote>();
            foreach (var selectedBeer in chosenBeers)
            {
                var beerFromDb = await GetBeer(selectedBeer);
                var vote = new Vote { VoterId = userId, BeerId = beerFromDb.BeerId };
                userVotes.Add(vote);
                await _genericRepository.PostAsync<Vote, VoteResponseDTO>(uri.ToString() , vote);
            }

            return userVotes;
        }

        private async Task<Beer> GetBeer(Beer beer)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BeerFromBattleEndpoint
            };

            var query = HttpUtility.ParseQueryString(uri.Query);
            query["BattleId"] = "1";
            query["AssignedNumberInBattle"] = beer.AssignedNumberInBattle.ToString();
            uri.Query = query.ToString();

            var beerFromDb = await _genericRepository.GetAsync<BeerFromBattleResponseDTO>(uri.ToString());
            var modelBeer =_mapper.Map<Beer>(beerFromDb);
            return modelBeer;
        }
    }
}
