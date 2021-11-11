using MediatR;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class RemoveBreweryRequest : IRequest<RemoveBreweryResponse>
    {
        public int breweryId { get; set; }
    }
}