using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.ErrorHandling;
using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Handlers
{
    public class GetBeerInBattleHandler : IRequestHandler<GetBeerInBattleRequest, GetBeerInBattleResponse>
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public GetBeerInBattleHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<GetBeerInBattleResponse> Handle(GetBeerInBattleRequest request, CancellationToken cancellationToken)
        {
            var query = new GetBeerInBattleQuery
            {
                BattleId = request.BattleId,
                BeerNumber = request.AssignedNumberInBattle
            };

            var beerFromDb = await _queryExecutor.Execute(query);
            if (beerFromDb == null)
            {
                return new GetBeerInBattleResponse
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var mappedBeer = _mapper.Map<Domain.Models.Beer>(beerFromDb);
            var response = new GetBeerInBattleResponse
            {
                Data = mappedBeer
            };

            return response;
        }
    }
}
