using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class GetBreweryByBeerIdRequest : IRequest<GetBreweryByBeerIdResponse>
    {
        public int BreweryId { get; set; }
    }
}
