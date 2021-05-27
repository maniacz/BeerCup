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
    public class AddBeerHandler : IRequestHandler<AddBeerRequest, AddBeerResponse>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly IMapper mapper;

        public AddBeerHandler(ICommandExecutor commandExecutor, IMapper mapper)
        {
            this.commandExecutor = commandExecutor;
            this.mapper = mapper;
        }

        public async Task<AddBeerResponse> Handle(AddBeerRequest request, CancellationToken cancellationToken)
        {
            var beer = this.mapper.Map<DataAccess.Entities.Beer>(request);
            var command = new AddBeerCommand()
            {
                Parameter = beer
            };
            var beerFromDb = await this.commandExecutor.Execute(command);

            return new AddBeerResponse()
            {
                Data = this.mapper.Map<Beer>(beerFromDb)
            };
        }
    }
}
