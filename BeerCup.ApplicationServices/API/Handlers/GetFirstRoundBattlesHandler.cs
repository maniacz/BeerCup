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
    public class GetFirstRoundBattlesHandler : IRequestHandler<GetFirstRoundBattlesRequest, GetFirstRoundBattlesResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQueryExecutor _queryExecutor;

        public GetFirstRoundBattlesHandler(IMapper mapper, IQueryExecutor queryExecutor)
        {
            _mapper = mapper;
            _queryExecutor = queryExecutor;
        }

        public async Task<GetFirstRoundBattlesResponse> Handle(GetFirstRoundBattlesRequest request, CancellationToken cancellationToken)
        {
            var query = new GetFirstRoundBattlesQuery();
            var firstRoundBattlesFromDb = await _queryExecutor.Execute(query);

            if (firstRoundBattlesFromDb == null)
            {
                return new GetFirstRoundBattlesResponse
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetFirstRoundBattlesResponse
            {
                Data = _mapper.Map<List<Battle>>(firstRoundBattlesFromDb)
            };
        }
    }
}
