using MediatR;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class GetFirstBattleFromRoutingRequest : IRequest<GetFirstBattleFromRoutingResponse>
    {
        public int NextBattleNo { get; set; }
    }
}