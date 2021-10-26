using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
using BeerCup.ApplicationServices.API.ErrorHandling;
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
    public class EndBattleHandler : IRequestHandler<EndBattleRequest, EndBattleResponse>
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public EndBattleHandler(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor, IMapper mapper)
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<EndBattleResponse> Handle(EndBattleRequest request, CancellationToken cancellationToken)
        {
            var query = new GetRunningBattleQuery
            {
                BattleId = request.Id
            };

            var runningBattle = await _queryExecutor.Execute(query);
            if (runningBattle == null)
            {
                return new EndBattleResponse
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            runningBattle.IsRunning = false;
            var command = new EndBattleCommand
            {
                Parameter = runningBattle
            };

            var battleFromDb = await _commandExecutor.Execute(command);
            if (battleFromDb == null)
            {
                return new EndBattleResponse
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }

            return new EndBattleResponse
            {
                Data = _mapper.Map<Battle>(battleFromDb)
            };
        }
    }
}
