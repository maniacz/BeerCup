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
    public class AlterBeerHandler : IRequestHandler<AlterBeerRequest, AlterBeerResponse>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly IMapper mapper;

        public AlterBeerHandler(ICommandExecutor commandExecutor, IMapper mapper)
        {
            this.commandExecutor = commandExecutor;
            this.mapper = mapper;
        }
        public async Task<AlterBeerResponse> Handle(AlterBeerRequest request, CancellationToken cancellationToken)
        {
            var beer = this.mapper.Map<DataAccess.Entities.Beer>(request);
            var command = new AlterBeerCommand()
            {
                Parameter = beer
            };

            var beerFromDb = await this.commandExecutor.Execute(command);

            return new AlterBeerResponse()
            {
                Data = this.mapper.Map<Beer>(beerFromDb)
            };
        }
    }
}
