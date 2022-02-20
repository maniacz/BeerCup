namespace BeerCup.ApplicationServices.API.Domain.Models
{
    public class LuckyVoter
    {
        public int VoterId { get; set; }

        public string Username { get; set; }

        public int BattleId { get; set; }

        public string BattleStyle { get; set; }

        public bool IsPaperVote { get; set; }
    }
}
