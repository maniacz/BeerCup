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
    public class EditBreweryNameHandler : IRequestHandler<EditBreweryNameRequest, EditBreweryNameResponse>
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public EditBreweryNameHandler(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor, IMapper mapper)
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<EditBreweryNameResponse> Handle(EditBreweryNameRequest request, CancellationToken cancellationToken)
        {
            var query = new GetBreweryByIdQuery
            {
                BreweryId = request.BreweryId
            };

            var breweryToUpdate = await _queryExecutor.Execute(query);
            breweryToUpdate.Name = request.BreweryName;
            var command = new EditBreweryNameCommand
            {
                Parameter = breweryToUpdate
            };

            var breweryFromDb = await _commandExecutor.Execute(command);
            if (breweryFromDb == null)
            {
                return new EditBreweryNameResponse
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new EditBreweryNameResponse
            {
                Data = _mapper.Map<Brewery>(breweryFromDb)
            };
        }
    }
}
