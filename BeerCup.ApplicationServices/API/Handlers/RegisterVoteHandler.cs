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
    public class RegisterVoteHandler : IRequestHandler<SendVoteRequest, SendVotesResponse>
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public RegisterVoteHandler(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor, IMapper mapper)
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<SendVotesResponse> Handle(SendVoteRequest request, CancellationToken cancellationToken)
        {
            var vote = _mapper.Map<DataAccess.Entities.Vote>(request);

            var command = new RegisterVoteCommand
            {
                Parameter = vote
            };

            command.Parameter.BreweryId = await GetBreweryId(request.BeerId);

            var voteFromDb = await _commandExecutor.Execute(command);

            return new SendVotesResponse
            {
                Data = _mapper.Map<Vote>(voteFromDb)
            };
        }

        private async Task<int> GetBreweryId(int beerId)
        {
            var query = new GetBreweryByBeerIdQuery
            {
                BeerId = beerId
            };

            var brewery = await _queryExecutor.Execute(query);

            return brewery.Id;
        }
    }
}
