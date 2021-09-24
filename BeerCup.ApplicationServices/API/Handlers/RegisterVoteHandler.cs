using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Commands;
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
        private readonly IMapper _mapper;

        public RegisterVoteHandler(ICommandExecutor commandExecutor, IMapper mapper)
        {
            _commandExecutor = commandExecutor;
            _mapper = mapper;
        }

        public async Task<SendVotesResponse> Handle(SendVoteRequest request, CancellationToken cancellationToken)
        {
            var vote = _mapper.Map<DataAccess.Entities.Vote>(request);

            var command = new RegisterVoteCommand
            {
                Parameter = vote
            };

            //get brewery by beerId

            var voteFromDb = await _commandExecutor.Execute(command);

            return new SendVotesResponse
            {
                Data = _mapper.Map<Vote>(voteFromDb)
            };
        }
    }
}
