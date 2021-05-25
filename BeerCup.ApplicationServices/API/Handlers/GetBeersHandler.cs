using BeerCup.ApplicationServices.API.Domain;
using BeerCup.DataAccess;
using BeerCup.DataAccess.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Handlers
{
    public class GetBeersHandler : IRequestHandler<GetBeersRequest, GetBeersResponse>
    {
        private readonly IRepository<DataAccess.Entities.Beer> repository;

        public GetBeersHandler(IRepository<DataAccess.Entities.Beer> repository)
        {
            this.repository = repository;
        }

        public async Task<GetBeersResponse> Handle(GetBeersRequest request, CancellationToken cancellationToken)
        {
            var beers = await this.repository.GetAll();
            var domainBeers = beers.Select(b => new Domain.Models.Beer()
            {
                BeerId = b.Id,
                BattleId = b.BattleId
            });

            var response = new GetBeersResponse()
            {
                Data = domainBeers.ToList()
            };

            return response;
        }
    }
}
