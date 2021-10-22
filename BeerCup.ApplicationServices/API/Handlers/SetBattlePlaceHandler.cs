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
    public class SetBattlePlaceHandler : IRequestHandler<AddBattlePlaceRequest, AddBattlePlaceResponse>
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IMapper _mapper;

        public SetBattlePlaceHandler(ICommandExecutor commandExecutor, IMapper mapper)
        {
            _commandExecutor = commandExecutor;
            _mapper = mapper;
        }

        public async Task<AddBattlePlaceResponse> Handle(AddBattlePlaceRequest request, CancellationToken cancellationToken)
        {
            var battlePlace = _mapper.Map<DataAccess.Entities.BattlePlace>(request);
            var command = new SetBattlePlaceCommand
            {
                Parameter = battlePlace
            };

            var battlePlaceFromDb = await _commandExecutor.Execute(command);
            if (battlePlaceFromDb == null)
            {
                return new AddBattlePlaceResponse
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new AddBattlePlaceResponse
            {
                Data = _mapper.Map<BattlePlace>(battlePlaceFromDb)
            };
        }
    }
}
