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
    public class PublishBattleResultsHandler : IRequestHandler<PublishResultsRequest, PublishResultsResponse>
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly ICommandExecutor _commandExecutor;
        private readonly IMapper _mapper;

        public PublishBattleResultsHandler(IQueryExecutor queryExecutor, ICommandExecutor commandExecutor, IMapper mapper)
        {
            _queryExecutor = queryExecutor;
            _commandExecutor = commandExecutor;
            _mapper = mapper;
        }

        public async Task<PublishResultsResponse> Handle(PublishResultsRequest request, CancellationToken cancellationToken)
        {
            var query = new GetBattleByNoQuery()
            {
                BattleNo = request.Id
            };

            var battleToPublish = await _queryExecutor.Execute(query);
            if (battleToPublish == null)
            {
                return new PublishResultsResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            if (request.ResultsPublished)
            {
                battleToPublish.ResultsPublished = true;
            }
            else
            {
                battleToPublish.ResultsPublished = false;
            }

            var command = new PublishBattleResultsCommand
            {
                Parameter = _mapper.Map<DataAccess.Entities.Battle>(battleToPublish)
            };

            var battleFromDb = await _commandExecutor.Execute(command);
            if (battleFromDb == null)
            {
                return new PublishResultsResponse
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }

            return new PublishResultsResponse
            {
                Data = _mapper.Map<Battle>(battleFromDb)
            };
        }
    }
}
