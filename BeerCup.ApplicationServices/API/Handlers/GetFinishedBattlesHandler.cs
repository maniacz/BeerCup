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
    public class GetFinishedBattlesHandler : IRequestHandler<GetFinishedBattlesResultsRequest, GetFinishedBattlesResponse>
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public GetFinishedBattlesHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }
        public async Task<GetFinishedBattlesResponse> Handle(GetFinishedBattlesResultsRequest request, CancellationToken cancellationToken)
        {
            var query = new GetFinishedBattlesQuery();

            var finishedBattles = await _queryExecutor.Execute(query);

            return new GetFinishedBattlesResponse
            {
                Data = _mapper.Map<List<Battle>>(finishedBattles)
            };
        }
    }
}
