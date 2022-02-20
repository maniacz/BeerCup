using BeerCup.DataAccess.Entities.Base;

namespace BeerCup.DataAccess.Entities
{
    public class LuckyVoter : EntityBase
    {
        public int BattleId { get; set; }

        public Battle Battle { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public bool IsPaperVote { get; set; }
    }
}
