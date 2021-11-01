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
    public class GetFirstBattleFromRoutingHandler : IRequestHandler<GetFirstBattleFromRoutingRequest, GetFirstBattleFromRoutingResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;

        public GetFirstBattleFromRoutingHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetFirstBattleFromRoutingResponse> Handle(GetFirstBattleFromRoutingRequest request, CancellationToken cancellationToken)
        {
            var query = new GetFirstBattleFromRoutingQuery()
            {
                NextBattleNo = request.NextBattleNo
            };
            
            var battle = await queryExecutor.Execute(query);
            if (battle == null)
            {
                return new GetFirstBattleFromRoutingResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var mappedBeer = this.mapper.Map<Battle>(battle);
            return new GetFirstBattleFromRoutingResponse()
            {
                Data = mappedBeer
            };
        }
    }
}
