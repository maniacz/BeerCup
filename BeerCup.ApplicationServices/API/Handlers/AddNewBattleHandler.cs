using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
using BeerCup.ApplicationServices.API.ErrorHandling;
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
    public class AddNewBattleHandler : IRequestHandler<AddNewBattleRequest, AddNewBattleResponse>
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IMapper _mapper;

        public AddNewBattleHandler(ICommandExecutor commandExecutor, IMapper mapper)
        {
            _commandExecutor = commandExecutor;
            _mapper = mapper;
        }

        public async Task<AddNewBattleResponse> Handle(AddNewBattleRequest request, CancellationToken cancellationToken)
        {
            var battle = _mapper.Map<DataAccess.Entities.Battle>(request);
            var command = new AddNewBattleCommand
            {
                Parameter = battle
            };

            var battleFromDb = await _commandExecutor.Execute(command);
            if (battleFromDb == null)
            {
                return new AddNewBattleResponse
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }

            return new AddNewBattleResponse
            {
                Data = _mapper.Map<Battle>(battleFromDb)
            };
        }
    }
}
