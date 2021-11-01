using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
