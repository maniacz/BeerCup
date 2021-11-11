using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
using BeerCup.ApplicationServices.API.ErrorHandling;
using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Handlers
{
    public class RemoveBreweryHandler : IRequestHandler<RemoveBreweryRequest, RemoveBreweryResponse>
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IMapper _mapper;
        private readonly ILogger<RemoveBreweryHandler> _logger;

        public RemoveBreweryHandler(ICommandExecutor commandExecutor, IMapper mapper, ILogger<RemoveBreweryHandler> logger)
        {
            _commandExecutor = commandExecutor;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RemoveBreweryResponse> Handle(RemoveBreweryRequest request, CancellationToken cancellationToken)
        {
            var command = new RemoveBreweryCommand
            {
                Parameter = new DataAccess.Entities.Brewery { Id = request.breweryId }
            };

            DataAccess.Entities.Brewery removedBreweryFromDb;
            try
            {
                removedBreweryFromDb = await _commandExecutor.Execute(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problem while removing brewery. \nRequest data: \n\tbrewery Id: {request.breweryId}");
                return new RemoveBreweryResponse
                {
                    Error = new ErrorModel(ex.Message)
                };
            }

            if (removedBreweryFromDb == null)
            {
                return new RemoveBreweryResponse
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new RemoveBreweryResponse
            {
                Data = _mapper.Map<Brewery>(removedBreweryFromDb)
            };
        }
    }
}
