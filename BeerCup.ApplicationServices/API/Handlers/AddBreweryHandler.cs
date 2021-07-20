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
    public class AddBreweryHandler : IRequestHandler<AddBreweryRequest, AddBreweryResponse>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly IMapper mapper;

        public AddBreweryHandler(ICommandExecutor commandExecutor, IMapper mapper)
        {
            this.commandExecutor = commandExecutor;
            this.mapper = mapper;
        }

        public async Task<AddBreweryResponse> Handle(AddBreweryRequest request, CancellationToken cancellationToken)
        {
            var brewery = this.mapper.Map<DataAccess.Entities.Brewery>(request);
            var command = new AddBreweryCommand()
            {
                Parameter = brewery
            };

            var breweryFromDb = await commandExecutor.Execute(command);

            return new AddBreweryResponse()
            {
                Data = this.mapper.Map<Brewery>(breweryFromDb)
            };
        }
    }
}
