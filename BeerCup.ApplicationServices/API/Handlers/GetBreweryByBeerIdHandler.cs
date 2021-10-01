using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.ErrorHandling;
using BeerCup.DataAccess;
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
    public class GetBreweryByBeerIdHandler : IRequestHandler<GetBreweryByBeerIdRequest, GetBreweryByBeerIdResponse>
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public GetBreweryByBeerIdHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<GetBreweryByBeerIdResponse> Handle(GetBreweryByBeerIdRequest request, CancellationToken cancellationToken)
        {
            var query = new GetBreweryByBeerIdQuery
            {
                BeerId = request.BreweryId
            };

            var brewery = await _queryExecutor.Execute(query);
            if (brewery == null)
            {
                return new GetBreweryByBeerIdResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var mappedBrewery = _mapper.Map<Domain.Models.Brewery>(brewery);
            var response = new GetBreweryByBeerIdResponse
            {
                Data = mappedBrewery
            };

            return response;
        }
    }
}
