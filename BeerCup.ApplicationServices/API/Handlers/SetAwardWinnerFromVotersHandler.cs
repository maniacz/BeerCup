using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.ErrorHandling;
using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Commands;
using BeerCup.DataAccess.CQRS.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Handlers
{
    public class SetAwardWinnerFromVotersHandler : IRequestHandler<SetAwardWinnerRequest, SetAwardWinnerResponse>
    {
        private const int PaperVoteUserId = 0;

        private readonly IMapper _mapper;
        private readonly IQueryExecutor _queryExecutor;
        private readonly ICommandExecutor _commandExecutor;
        private readonly ILogger _logger;

        public SetAwardWinnerFromVotersHandler(IMapper mapper, IQueryExecutor queryExecutor, ICommandExecutor commandExecutor, ILogger<SetAwardWinnerFromVotersHandler> logger)
        {
            _mapper = mapper;
            _queryExecutor = queryExecutor;
            _commandExecutor = commandExecutor;
            _logger = logger;
        }

        public async Task<SetAwardWinnerResponse> Handle(SetAwardWinnerRequest request, CancellationToken cancellationToken)
        {
            var luckyVoter = new DataAccess.Entities.LuckyVoter
            {
                BattleId = request.BattleId,
            };

            if (!request.IsPaperVoteWinner)
            {
                var query = new GetVotersFromBattleQuery
                {
                    BattleId = request.BattleId
                };

                var voters = await _queryExecutor.Execute(query);

                if (voters is null)
                {
                    return new SetAwardWinnerResponse
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
                    };
                }

                var random = new Random();
                var winner = voters.ElementAt(random.Next(voters.Count));

                luckyVoter.UserId = winner.Id;
            }
            else
            {
                luckyVoter.UserId = PaperVoteUserId;
            }

            var previousLuckyVoterQuery = new GetLuckyVoterQuery
            {
                BattleId = request.BattleId
            };
            var previousLuckyVoterToUpdate = await _queryExecutor.Execute(previousLuckyVoterQuery);

            if (previousLuckyVoterToUpdate != null)
            {
                previousLuckyVoterToUpdate.UserId = luckyVoter.UserId;
                var command = new EditLuckyVoterCommand
                {
                    Parameter = previousLuckyVoterToUpdate
                };

                var luckyVoterFromDb = await _commandExecutor.Execute(command);
                if (luckyVoterFromDb == null)
                {
                    return new SetAwardWinnerResponse
                    {
                        Error = new ErrorModel(ErrorType.InternalServerError)
                    };
                }

                return new SetAwardWinnerResponse
                {
                    Data = _mapper.Map<Domain.Models.LuckyVoter>(luckyVoterFromDb)
                };
            }
            else
            {
                var command = new AddLuckyUserCommand { Parameter = luckyVoter };

                var luckyVoterFromDb = await _commandExecutor.Execute(command);
                if (luckyVoterFromDb is null)
                {
                    return new SetAwardWinnerResponse
                    {
                        Error = new ErrorModel(ErrorType.InternalServerError)
                    };
                }

                return new SetAwardWinnerResponse
                {
                    Data = _mapper.Map<Domain.Models.LuckyVoter>(luckyVoterFromDb)
                };
            }
        }
    }
}
