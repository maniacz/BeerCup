using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class GetBeersRequest : IRequest<GetBeersResponse> 
    {
        public int BattleId { get; set; }
    }
}
