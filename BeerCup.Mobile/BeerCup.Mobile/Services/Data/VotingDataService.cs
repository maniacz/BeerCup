using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Exceptions;
using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BeerCup.Mobile.Services.Data
{
    public class VotingDataService : IVotingDataService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ISettingsService _settingsService;
        private readonly IBattleDataService _battleDataService;

        private int _runningBattleId;

        public VotingDataService(IGenericRepository genericRepository, ISettingsService settingsService, IBattleDataService battleDataService)
        {
            _genericRepository = genericRepository;
            _settingsService = settingsService;
            _battleDataService = battleDataService;
        }

        public async Task<IEnumerable<Vote>> SendVotes(IEnumerable<Beer> chosenBeers)
        {
            var userId = _settingsService.UserIdSetting;
            UriBuilder uri = await SetUriForCurrentBattleUserVotes();

            //user has already voted in current battle
            var userVotesInCurrentBattle = await _genericRepository.GetAsync<ApiResponse<List<Vote>>>(uri.ToString());
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

            List<Vote> userVotes = new List<Vote>();
            try
            {
                foreach (var selectedBeer in chosenBeers)
                {
                    var beerFromDb = await GetBeer(selectedBeer);
                    var vote = new Vote { VoterId = userId, BeerId = beerFromDb.BeerId, BattleId = beerFromDb.BattleId };
                    //todo: poniżej może zadziałać indeks na tabeli na unikalność głosów, trza by to obsłużyć
                    var response = await _genericRepository.PostAsync<Vote, ApiResponse<Vote>>(uri.ToString(), vote);
                    userVotes.Add(response.Data);
                }
            }
            catch (Exception ex)
            {
                if (userVotes.Count > 0)
                    await RemoveRemainingBattleUserVotes();

                Debug.WriteLine($"Sending votes failed. {ex.Message}");
            }

            return userVotes;
        }

        private async Task RemoveRemainingBattleUserVotes()
        {
            UriBuilder uri = await SetUriForCurrentBattleUserVotes();

            //todo: przetestować bo była zmiana w genericRepo z DeleteAsync na DeleteAsync<T>
            await _genericRepository.DeleteAsync<List<Vote>>(uri.ToString());
        }

        private async Task<UriBuilder> SetUriForCurrentBattleUserVotes()
        {
            var userId = _settingsService.UserIdSetting.ToString();
            var currentRunningBattle = await _battleDataService.GetCurrentRunningBattle();
            var battleId = currentRunningBattle.Id;
            _settingsService.RunningBattleIdSetting = battleId;

            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.UserBattleVotesEndpoint.Replace("{battleId}", battleId.ToString()).Replace("{userId}", userId)
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
            query["BattleId"] = _settingsService.RunningBattleIdSetting.ToString();
            query["AssignedNumberInBattle"] = beer.AssignedNumberInBattle.ToString();
            uri.Query = query.ToString();

            var response = await _genericRepository.GetAsync<ApiResponse<Beer>>(uri.ToString());
            return response.Data;
        }
    }
}
