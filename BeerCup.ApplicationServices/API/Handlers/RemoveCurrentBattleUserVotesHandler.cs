using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Commands;
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
    public class RemoveCurrentBattleUserVotesHandler : IRequestHandler<RemoveUserVotesRequest, RemoveUserVotesResponse>
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public RemoveCurrentBattleUserVotesHandler(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor, IMapper mapper)
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<RemoveUserVotesResponse> Handle(RemoveUserVotesRequest request, CancellationToken cancellationToken)
        {
            var response = new RemoveUserVotesResponse()
            {
                Data = new List<Vote>()
            };

            List<DataAccess.Entities.Vote> votesToRemove = await GetVotesToRemove(request);

            foreach (var vote in votesToRemove)
            {
                var command = new RemoveVoteCommand()
                {
                    Parameter = vote
                };
                var voteFromDb = await _commandExecutor.Execute(command);

                response.Data.Add(_mapper.Map<Vote>(voteFromDb));
            }

            return response;
        }

        private async Task<List<DataAccess.Entities.Vote>> GetVotesToRemove(RemoveUserVotesRequest request)
        {
            var query = new GetUserVotesInBattleQuery
            {
                BattleId = request.BattleId,
                UserId = request.UserId
            };

            return await _queryExecutor.Execute(query);
        }
    }
}
