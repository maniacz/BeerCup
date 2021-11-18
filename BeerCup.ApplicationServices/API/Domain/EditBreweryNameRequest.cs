using MediatR;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class EditBreweryNameRequest : IRequest<EditBreweryNameResponse>
    {
        public int BreweryId { get; set; }
        public string Name { get; set; }
    }
}