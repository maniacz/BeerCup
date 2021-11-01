using BeerCup.ApplicationServices.API.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class PublishResultsRequest : IRequest<PublishResultsResponse>
    {
        public int BattleNo { get; set; }

        public bool ResultsPublished { get; set; }

        public bool WinnersPromotedToNextRound { get; set; }
    }
}
