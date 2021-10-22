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

        public async Task<IEnumerable<VoteResponse>> SendVotes(IEnumerable<Beer> chosenBeers)
        {
            var userId = _settingsService.UserIdSetting;
            UriBuilder uri = SetUriForCurrentBattleUserVotes();

            //user has already voted in current battle
            var userVotesInCurrentBattle = await _genericRepository.GetAsync<VotesListResponse>(uri.ToString());
            if (userVotesInCurrentBattle.Data.Count > 0)
            {
                if (userVotesInCurrentBattle.Data.Count != 2)
                {
                    await RemoveRemainingBattleUserVotes();
                    throw new ServiceVotingException();
                }
                throw new UserHasAlreadyVotedException();
            }


            uri.Path = ApiConstants.BattlesEndpoint;

            List<VoteResponse> userVotes = new List<VoteResponse>();
            try
            {
                foreach (var selectedBeer in chosenBeers)
                {
                    var beerFromDb = await GetBeer(selectedBeer);
                    var vote = new Vote { VoterId = userId, BeerId = beerFromDb.BeerId, BattleId = beerFromDb.Battle.BattleId };
                    //todo: poniżej może zadziałać indeks na tabeli na unikalność głosów, trza by to obsłużyć
                    var savedVote = await _genericRepository.PostAsync<Vote, VoteResponse>(uri.ToString() , vote);
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
            UriBuilder uri = SetUriForCurrentBattleUserVotes();

            await _genericRepository.DeleteAsync(uri.ToString());
        }

        private UriBuilder SetUriForCurrentBattleUserVotes()
        {
            var userId = _settingsService.UserIdSetting.ToString();
            //todo: zrób ustawianie battleId w _settingsService
            string battleId = "1";

            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.UserBattleVotesEndpoint.Replace("{battleId}", battleId).Replace("{userId}", userId)
            };
            return uri;
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

            var beerFromDb = await _genericRepository.GetAsync<BeerFromBattleResponse>(uri.ToString());
            var modelBeer =_mapper.Map<Beer>(beerFromDb);
            return modelBeer;
        }
    }
}
