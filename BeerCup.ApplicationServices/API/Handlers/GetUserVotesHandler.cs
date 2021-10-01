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
    public class GetUserVotesHandler : IRequestHandler<GetUserVotesRequest, GetUserVotesResponse>
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public GetUserVotesHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<GetUserVotesResponse> Handle(GetUserVotesRequest request, CancellationToken cancellationToken)
        {
            var query = new GetVotesQuery
            {
                BattleId = request.BattleId,
                UserId = request.UserId
            };

            var votesFromDb = await _queryExecutor.Execute(query);

            var votes = new List<Vote>();
            foreach (var vote in votesFromDb)
            {
                votes.Add(_mapper.Map<Vote>(vote));
            }

            var response = new GetUserVotesResponse
            {
                Data = votes
            };

            return response;
        }
    }
}
