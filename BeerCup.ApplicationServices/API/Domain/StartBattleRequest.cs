using BeerCup.ApplicationServices.API.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class StartBattleRequest : IRequest<StartBattleResponse>
    {
        public BattlePlace Place { get; set; }
    }
}
