using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Services.Data
{
    public class AdminPanelDataService : IAdminPanelDataService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IBattleDataService _battleDataService;
        private readonly INavigationService _navigationService;

        public AdminPanelDataService(IGenericRepository genericRepository, IBattleDataService battleDataService, INavigationService navigationService)
        {
            _genericRepository = genericRepository;
            _battleDataService = battleDataService;
            _navigationService = navigationService;
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

            var response = await _genericRepository.PutAsync(uri.ToString(), battle);
            return response?.Data;
        }

        public async Task<Battle> EndBattle(Battle battle)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/EndBattle"
            };

            var response = await _genericRepository.PutAsync(uri.ToString(), battle);
            return response?.Data;
        }

        public async Task<Battle> PublishResults(Battle battle)
        {
            battle.ResultsPublished = true;
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/PublishResults"
            };

            var response = await _genericRepository.PutAsync(uri.ToString(), battle);
            return response?.Data;
        }

        public async Task<Battle> HideResults(Battle battle)
        {
            battle.ResultsPublished = false;
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/PublishResults"
            };

            var response = await _genericRepository.PutAsync(uri.ToString(), battle);
            return response?.Data;
        }

        public async Task PromoteWinnersToFollowingBattles(Battle runningBattle)
        {
            UriBuilder battleRoutingUri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/Routing/" + runningBattle.BattleNo
            };

            var routingResponse = await _genericRepository.GetAsync<BattleRouting>(battleRoutingUri.ToString());
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

                var battleResponse = await _genericRepository.GetAsync<Battle>(battleUri.ToString());
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

                //todo: Przerobić na _genericRepository.PostAsync<Beer, ApiResponse<Beer>>
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

            var response = await _genericRepository.GetAsync<Battle>(uri.ToString());

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

            return response?.Data;
        }

        public async Task<Battle> SaveEditBattle(Battle battle)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AdminPanelEndpoint + "/EditBattle"
            };

            var response = await _genericRepository.PutAsync(uri.ToString(), battle);

            return response?.Data;
        }

        public async Task<List<Brewery>> GetAllBreweries()
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BreweriesEndpoint
            };

            var response = await _genericRepository.GetAsync<List<Brewery>>(uri.ToString());

            return response?.Data;
        }

        public async Task<Brewery> DeleteBrewery(Brewery brewery)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BreweriesEndpoint + "/" + brewery.BreweryId
            };

            var response = await _genericRepository.DeleteAsync<Brewery>(uri.ToString());

            return response?.Data;
        }

        public async Task<Brewery> EditBreweryName(Brewery brewery)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BreweriesEndpoint
            };

            var response = await _genericRepository.PutAsync<Brewery, Brewery>(uri.ToString(), brewery);

            return response?.Data;
        }

        public async Task<Brewery> AddNewBrewery(Brewery brewery)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BreweriesEndpoint
            };

            var response = await _genericRepository.PostAsync<Brewery, Brewery>(uri.ToString(), brewery);

            return response?.Data;
        }

        public async Task<LuckyVoter> GetLuckyVoter(int battleId)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AwardDrawingEndpoint + battleId
            };

            var response = await _genericRepository.GetAsync<LuckyVoter>(uri.ToString());

            return response?.Data;
        }

        public async Task<LuckyVoter> DrawLuckyVoter(int battleId, int paperVotesCount)
        {
            UriBuilder uri = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AwardDrawingWithPaperVotesEndpoint + battleId + "/" + paperVotesCount
            };

            var response = await _genericRepository.PostAsync<Battle, LuckyVoter>(uri.ToString(), null);

            return response?.Data;
        }
    }
}
