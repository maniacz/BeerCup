using MediatR;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class GetBattleRoutingRequest : IRequest<GetBattleRoutingResponse>
    {
        public int BattleId { get; set; }
    }
}