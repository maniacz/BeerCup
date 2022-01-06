using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetLuckyVoterQuery : QueryBase<LuckyVoter>
    {
        public int battleId { get; set; }

        public override async Task<LuckyVoter> Execute(BeerCupStorageContext context)
        {
            return await context.LuckyVoters.SingleOrDefaultAsync(v => v.BattleId == battleId);
        }
    }
}