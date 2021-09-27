using AutoMapper;
using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Exceptions;
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

        public async Task<IEnumerable<VoteResponseDTO>> SendVotes(IEnumerable<Beer> chosenBeers)
        {
            var userId = _settingsService.UserIdSetting;

            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.UserBattleVotesEndpoint
            };

            //user has already voted in current battle
            var userVotesInCurrentBattle = await _genericRepository.GetAsync<List<Vote>>(uri.ToString());
            if (userVotesInCurrentBattle.Count > 0)
            {
                if (userVotesInCurrentBattle.Count != 2)
                {
                    await RemoveRemainingBattleUserVotes();
                    throw new ServiceVotingException();
                }
                throw new UserHasAlreadyVotedException();
            }


            uri.Path = ApiConstants.BattlesEndpoint;

            List<VoteResponseDTO> userVotes = new List<VoteResponseDTO>();
            try
            {
                foreach (var selectedBeer in chosenBeers)
                {
                    var beerFromDb = await GetBeer(selectedBeer);
                    var vote = new Vote { VoterId = userId, BeerId = beerFromDb.BeerId };
                    var savedVote = await _genericRepository.PostAsync<Vote, VoteResponseDTO>(uri.ToString() , vote);
                    userVotes.Add(savedVote);
                }
            }
            catch (Exception)
            {
                if (userVotes.Count > 0)
                    await RemoveRemainingBattleUserVotes();

                throw;
            }

            return userVotes;
        }

        private async Task RemoveRemainingBattleUserVotes()
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.UserBattleVotesEndpoint
            };

            await _genericRepository.DeleteAsync(uri.ToString());
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
