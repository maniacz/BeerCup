using BeerCup.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Contracts.Services.Data
{
    public interface IBattleDataService
    {
        Task<Battle> GetCurrentRunningBattle();

        Task<Battle> GetTodaysBattle();

        Task<List<Result>> GetBattleResults(int battleId);

        Task<List<Battle>> GetFinishedBattles();

        Task<List<Vote>> GetBattleUserVotes(int battleId);

        Task<List<Battle>> GetAllBattles();

        Task<List<Brewery>> GetBreweriesFromBattle(int battleNo);

        Task<Battle> GetBattleByBattleNo(int battleNo);

        Task<List<Battle>> GetFirstRoundBattles();
    }
}
