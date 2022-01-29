using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetLuckyVoterQuery : QueryBase<LuckyVoter>
    {
        public int BattleId { get; set; }

        public override async Task<LuckyVoter> Execute(BeerCupStorageContext context)
        {
            return await context.LuckyVoters.Include(v => v.User).Include(v => v.Battle).SingleOrDefaultAsync(v => v.BattleId == BattleId);
        }
    }
}