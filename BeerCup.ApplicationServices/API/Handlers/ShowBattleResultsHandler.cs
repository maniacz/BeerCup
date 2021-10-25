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
    public class ShowBattleResultsHandler : IRequestHandler<ShowBattleResultRequest, ShowBattleResultResponse>
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public ShowBattleResultsHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<ShowBattleResultResponse> Handle(ShowBattleResultRequest request, CancellationToken cancellationToken)
        {
            var query = new ShowBattleResultsQuery
            {
                BattleId = request.BattleId
            };

            var results = await _queryExecutor.Execute(query);

            return new ShowBattleResultResponse
            {
                Data = _mapper.Map<List<BattleResult>>(results)
            };
        }
    }
}
