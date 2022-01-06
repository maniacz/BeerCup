using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
using BeerCup.ApplicationServices.API.ErrorHandling;
using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Commands;
using BeerCup.DataAccess.CQRS.Queries;
using BeerCup.DataAccess.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Handlers
{
    public class DrawAwardWinnerFromVotersHandler : IRequestHandler<DrawAwardWinnerRequest, DrawAwardWinnerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQueryExecutor _queryExecutor;
        private readonly ICommandExecutor _commandExecutor;
        private readonly ILogger _logger;

        public DrawAwardWinnerFromVotersHandler(IMapper mapper, IQueryExecutor queryExecutor, ICommandExecutor commandExecutor, ILogger<DrawAwardWinnerFromVotersHandler> logger)
        {
            _mapper = mapper;
            _queryExecutor = queryExecutor;
            _commandExecutor = commandExecutor;
            _logger = logger;
        }

        public async Task<DrawAwardWinnerResponse> Handle(DrawAwardWinnerRequest request, CancellationToken cancellationToken)
        {
            var query = new GetVotersFromBattleQuery
            {
                BattleId = request.BattleId
            };

            var voters = await _queryExecutor.Execute(query);

            if (voters is null)
            {
                return new DrawAwardWinnerResponse
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var random = new Random();
            var winner = voters.ElementAt(random.Next(voters.Count));

            var luckyVoter = new DataAccess.Entities.LuckyVoter 
            { 
                BattleId = request.BattleId, 
                UserId = winner.Id 
            };
            var command = new AddLuckyUserCommand { Parameter = luckyVoter };

            var luckyVoterFromDb = await _commandExecutor.Execute(command);
            if (luckyVoterFromDb is null)
            {
                return new DrawAwardWinnerResponse
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }

            return new DrawAwardWinnerResponse 
            {
                Data = _mapper.Map<Domain.Models.LuckyVoter>(luckyVoterFromDb)
            };
        }
    }
}
