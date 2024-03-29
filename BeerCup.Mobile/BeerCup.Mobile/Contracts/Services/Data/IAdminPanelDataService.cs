﻿using BeerCup.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Contracts.Services.Data
{
    public interface IAdminPanelDataService
    {
        Task<Battle> StartBattle(BattlePlace battlePlace);

        Task<Battle> EndBattle(Battle battle);

        Task<Battle> PublishResults(Battle battle);

        Task<Battle> HideResults(Battle battle);

        Task PromoteWinnersToFollowingBattles(Battle runningBattle);

        Task<bool> IsWinnersAlreadyPromotedToNextRound(Battle battle);

        Task<Battle> AddNewBattle(Battle battle);

        Task<Battle> SaveEditBattle(Battle battle);

        Task<List<Brewery>> GetAllBreweries();

        Task<Brewery> DeleteBrewery(Brewery brewery);

        Task<Brewery> EditBreweryName(Brewery brewery);

        Task<Brewery> AddNewBrewery(Brewery brewery);

        Task<LuckyVoter> GetLuckyVoter(int battleId);

        Task<LuckyVoter> DrawLuckyVoter(int battleId, int paperVotesCount);
    }
}
