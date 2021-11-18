using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeerCup.Mobile.Services.Data
{
    public class AdminPanelDataService : IAdminPanelDataService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IBattleDataService _battleDataService;

        public AdminPanelDataService(IGenericRepository genericRepository, IBattleDataService battleDataService)
        {
            _genericRepository = genericRepository;
            _battleDataService = battleDataService;
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

        public async Task PromoteWinnersToFollowingBattles(Battle runningBattle)
        {
            UriBuilder battleRoutingUri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/Routing/" + runningBattle.BattleNo
            };

            var routingResponse = await _genericRepository.GetAsync<ApiResponse<BattleRouting>>(battleRoutingUri.ToString());
            var nextBattleNo = routingResponse.Data.ToBattleNo;

            var finishedBattleResults = await _battleDataService.GetBattleResults(runningBattle.Id);
            List<Brewery> winners = GetFirstTwoBreweries(finishedBattleResults);

            if (routingResponse.Data.IsSecondBattle)
            {
                var betterThirdPlace = await GetBetterBreweryFromThirdPlace(nextBattleNo, finishedBattleResults);
                winners.Add(betterThirdPlace);
            }

            foreach (var breweryQualifiedToNextRound in winners)
            {
                UriBuilder battleUri = new UriBuilder(ApiConstants.BaseApiUrl)
                {
                    Path = ApiConstants.BattlesEndpoint + "/" + nextBattleNo
                };

                var battleResponse = await _genericRepository.GetAsync<ApiResponse<Battle>>(battleUri.ToString());
                var nextRoundBattleId = battleResponse?.Data.Id;

                var newBeer = new Beer
                {

                    BattleId = nextRoundBattleId.Value,
                    BreweryId = breweryQualifiedToNextRound.BreweryId
                };

                UriBuilder beerForNextRoundUri = new UriBuilder(ApiConstants.BaseApiUrl)
                {
                    Path = ApiConstants.BeersEndpoint
                };

                await _genericRepository.PostAsync(beerForNextRoundUri.ToString(), newBeer);
            }

            SetWinnersPromotedToNextRoundToTrue(runningBattle);
        }

        private async Task<Brewery> GetBetterBreweryFromThirdPlace(int nextBattleNo, List<Result> finishedBattleResults)
        {
            Battle firstBattle = await GetFirstBattleFromPair(nextBattleNo);
            var firstBattleResults = await _battleDataService.GetBattleResults(firstBattle.Id);

            Result thirdPlaceFromFirstBattle = firstBattleResults.Where(r => r.FinalRank == 3).FirstOrDefault();
            Result thirdPlaceFromSecondBattle = finishedBattleResults.Where(r => r.FinalRank == 3).FirstOrDefault();

            if (thirdPlaceFromFirstBattle.Precentage == thirdPlaceFromSecondBattle.Precentage)
            {
                //todo: i co teraz jak mamy po równo z trzecich miejsc?
            }

            if (thirdPlaceFromFirstBattle.Precentage > thirdPlaceFromSecondBattle.Precentage)
                return thirdPlaceFromFirstBattle.Brewery;
            else
                return thirdPlaceFromSecondBattle.Brewery;
        }

        private async Task<Battle> GetFirstBattleFromPair(int nextBattleNo)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/FirstBattleFromRouting/" + nextBattleNo
            };

            var response = await _genericRepository.GetAsync<ApiResponse<Battle>>(uri.ToString());

            return response?.Data;
        }

        private List<Brewery> GetFirstTwoBreweries(List<Result> battleResults)
        {
            return battleResults.Where(r => r.FinalRank < 3).Select(r => r.Brewery).ToList();
        }

        private async void SetWinnersPromotedToNextRoundToTrue(Battle runningBattle)
        {
            runningBattle.WinnersPromotedToNextRound = true;
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/PublishResults"
            };

            await _genericRepository.PutAsync(uri.ToString(), runningBattle);
        }

        public async Task<bool> IsWinnersAlreadyPromotedToNextRound(Battle battle)
        {
            var finishedBattle = await _battleDataService.GetBattleByBattleNo(battle.BattleNo);
            if (finishedBattle.WinnersPromotedToNextRound)
                return true;
            else
                return false;
        }

        public async Task<Battle> AddNewBattle(Battle battle)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/AddNewBattle"
            };

            var response = await _genericRepository.PostAsync(uri.ToString(), battle);

            return response;
        }

        public async Task<Battle> SaveEditBattle(Battle battle)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/EditBattle"
            };

            var response = await _genericRepository.PutAsync(uri.ToString(), battle);

            return response;
        }

        public async Task<List<Brewery>> GetAllBreweries()
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BreweriesEndpoint
            };

            var response = await _genericRepository.GetAsync<ApiResponse<List<Brewery>>>(uri.ToString());

            return response?.Data;
        }

        public async Task<Brewery> DeleteBrewery(Brewery brewery)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BreweriesEndpoint + "/" + brewery.BreweryId
            };

            var response = await _genericRepository.DeleteAsync<ApiResponse<Brewery>>(uri.ToString());

            return response?.Data;
        }

        public async Task<Brewery> EditBreweryName(Brewery brewery)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BreweriesEndpoint
            };

            var response = await _genericRepository.PutAsync<Brewery, ApiResponse<Brewery>>(uri.ToString(), brewery);

            return response?.Data;
        }

        public async Task<Brewery> AddNewBrewery(Brewery brewery)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BreweriesEndpoint
            };

            var response = await _genericRepository.PostAsync<Brewery, ApiResponse<Brewery>>(uri.ToString(), brewery);

            return response?.Data;
        }
    }
}
