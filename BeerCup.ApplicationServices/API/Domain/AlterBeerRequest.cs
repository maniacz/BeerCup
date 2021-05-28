using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class AlterBeerRequest : IRequest<AlterBeerResponse>
    {
        public int BeerId { get; set; }
        public int BattleId { get; set; }
        public int BreweryId { get; set; }
        public int NumberInBattle { get; set; }
    }
}
