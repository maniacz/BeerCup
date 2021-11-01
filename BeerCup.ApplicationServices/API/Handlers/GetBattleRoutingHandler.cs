using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
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
    public class GetBattleRoutingHandler : IRequestHandler<GetBattleRoutingRequest, GetBattleRoutingResponse>
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public GetBattleRoutingHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<GetBattleRoutingResponse> Handle(GetBattleRoutingRequest request, CancellationToken cancellationToken)
        {
            var query = new GetBattleRoutingQuery
            {
                BattleId = request.BattleId
            };

            var routingFromDb = await _queryExecutor.Execute(query);
            if (routingFromDb == null)
            {
                return new GetBattleRoutingResponse
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var mappedRouting = _mapper.Map<BattleRouting>(routingFromDb);
            return new GetBattleRoutingResponse
            {
                Data = mappedRouting
            };
        }
    }
}
