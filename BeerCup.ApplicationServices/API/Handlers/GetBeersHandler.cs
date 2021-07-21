using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.ErrorHandling;
using BeerCup.ApplicationServices.Components.OpenWeather;
using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Queries;
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
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly IWeatherConnector weatherConnector;

        public GetBeersHandler(IQueryExecutor queryExecutor, IMapper mapper, IWeatherConnector weatherConnector)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.weatherConnector = weatherConnector;
        }

        public async Task<GetBeersResponse> Handle(GetBeersRequest request, CancellationToken cancellationToken)
        {
            //todo: Finalnie wywal, to było tylko do obczajenia jak działa strzelanie do endpointów api.
            var weather = await this.weatherConnector.Fetch("Bielsko-Biala");

            var query = new GetBeersQuery()
            {
                BattleId = request.BattleId
            };

            var beers = await this.queryExecutor.Execute(query);
            if (beers == null)
            {
                return new GetBeersResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            //todo: jest już automaper więc poniższe mapowanie można chyba wywalić
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
