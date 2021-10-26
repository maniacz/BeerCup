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
        public int Id { get; set; }

        public string Style { get; set; }

        public List<Beer> Beers { get; set; }

        public BattlePlace Place { get; set; }

        public bool ResultsPublished { get; set; }
    }
}
