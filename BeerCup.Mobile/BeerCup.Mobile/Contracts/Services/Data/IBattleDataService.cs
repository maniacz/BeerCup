using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BeerCup.Mobile.Contracts.Services.Data
{
    public interface IBattleDataService
    {
        Task<Battle> GetCurrentRunningBattle();

        Task<Battle> GetTodaysBattle();

        Task<List<Result>> GetBattleResults(int battleId);

        Task<List<Battle>> GetFinishedBattles();
    }
}
