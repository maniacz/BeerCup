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
    public class GetTodaysBattleHandler : IRequestHandler<GetTodaysBattleRequest, GetTodaysBattleResponse>
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public GetTodaysBattleHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<GetTodaysBattleResponse> Handle(GetTodaysBattleRequest request, CancellationToken cancellationToken)
        {
            var query = new GetTodaysBattleQuery();

            var battleFromDb = await _queryExecutor.Execute(query);
            if (battleFromDb == null)
            {
                return new GetTodaysBattleResponse
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var mappedBattle = _mapper.Map<Battle>(battleFromDb);
            var response = new GetTodaysBattleResponse
            {
                Data = mappedBattle
            };

            return response;
        }
    }
}
