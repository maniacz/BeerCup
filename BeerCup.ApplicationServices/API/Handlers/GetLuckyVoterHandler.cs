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
    internal class GetLuckyVoterHandler : IRequestHandler<GetLuckyVoterRequest, GetLuckyVoterResponse>
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public GetLuckyVoterHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<GetLuckyVoterResponse> Handle(GetLuckyVoterRequest request, CancellationToken cancellationToken)
        {
            var query = new GetLuckyVoterQuery
            {
                battleId = request.battleId
            };

            var luckyVoterFromDb = await _queryExecutor.Execute(query);
            if (luckyVoterFromDb == null)
            {
                return new GetLuckyVoterResponse
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var luckyVoter = _mapper.Map<LuckyVoter>(luckyVoterFromDb);

            return new GetLuckyVoterResponse
            {
                Data = luckyVoter
            };
        }
    }
}
