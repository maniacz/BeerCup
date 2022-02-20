using MediatR;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class SetAwardWinnerRequest : IRequest<SetAwardWinnerResponse>
    {
        public bool IsPaperVoteWinner { get; set; }
        public int BattleId { get; set; }
    }
}
