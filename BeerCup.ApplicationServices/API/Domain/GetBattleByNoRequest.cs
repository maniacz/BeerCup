using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class GetBattleByNoRequest : IRequest<GetBattleByNoResponse>
    {
        public int BattleId { get; set; }
    }
}
