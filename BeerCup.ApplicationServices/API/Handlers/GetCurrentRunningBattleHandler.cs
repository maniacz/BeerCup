using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
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
    public class GetCurrentRunningBattleHandler : IRequestHandler<GetCurrentRunningBattleRequest, GetCurrentRunningBattleResponse>
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public GetCurrentRunningBattleHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<GetCurrentRunningBattleResponse> Handle(GetCurrentRunningBattleRequest request, CancellationToken cancellationToken)
        {
            var query = new GetCurrentBattleQuery();

            var runningBattle = await _queryExecutor.Execute(query);
            var mappedBatte = _mapper.Map<Battle>(runningBattle);

            var response = new GetCurrentRunningBattleResponse
            {
                Data = mappedBatte
            };

            return response;
        }
    }
}
