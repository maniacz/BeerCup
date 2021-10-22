using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class AddBattlePlaceRequest : IRequest<AddBattlePlaceResponse>
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
