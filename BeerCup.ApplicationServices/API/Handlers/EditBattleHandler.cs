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
    public class EditBattleHandler : IRequestHandler<EditBattleRequest, EditBattleResponse>
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public EditBattleHandler(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor, IMapper mapper)
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<EditBattleResponse> Handle(EditBattleRequest request, CancellationToken cancellationToken)
        {
            var query = new GetBattleByNoQuery
            {
                BattleNo = request.BattleNo
            };

            var battleToUpdate = await _queryExecutor.Execute(query);
            if (battleToUpdate == null)
            {
                return new EditBattleResponse
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            battleToUpdate.BattleNo = request.BattleNo;
            battleToUpdate.BattleName = request.BattleName;
            battleToUpdate.Style = request.Style;
            battleToUpdate.PubName = request.PubName;
            battleToUpdate.Date = request.Date;

            var command = new EditBattleCommand
            {
                Parameter = battleToUpdate
            };

            var battleFromDb = await _commandExecutor.Execute(command);
            if (battleFromDb == null)
            {
                return new EditBattleResponse
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }

            return new EditBattleResponse
            {
                Data = _mapper.Map<Battle>(battleFromDb)
            };
        }
    }
}
