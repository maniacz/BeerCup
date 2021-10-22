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
    public class StartBattleHandler : IRequestHandler<StartBattleRequest, StartBattleResponse>
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public StartBattleHandler(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor, IMapper mapper)
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<StartBattleResponse> Handle(StartBattleRequest request, CancellationToken cancellationToken)
        {
            var query = new GetTodaysBattleQuery();
            var battleToStart = await _queryExecutor.Execute(query);
            if (battleToStart == null)
            {
                return new StartBattleResponse
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            battleToStart.IsRunning = true;
            battleToStart.Place = _mapper.Map<DataAccess.Entities.BattlePlace>(request.Place);
            var command = new StartBattleCommand
            {
                Parameter = battleToStart
            };

            var battleFromDb = await _commandExecutor.Execute(command);

            return new StartBattleResponse
            {
                Data = _mapper.Map<Battle>(battleFromDb)
            };
        }
    }
}
