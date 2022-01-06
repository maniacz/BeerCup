using MediatR;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class GetLuckyVoterRequest : IRequest<GetLuckyVoterResponse>
    {
        public int battleId { get; set; }
    }
}