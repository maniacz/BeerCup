using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
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
    public class GetNotAssignedBreweriesHandler : IRequestHandler<GetNotAssignedBreweriesRequest, GetNotAssignedBreweriesResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQueryExecutor _queryExecutor;

        public GetNotAssignedBreweriesHandler(IMapper mapper, IQueryExecutor queryExecutor)
        {
            _mapper = mapper;
            _queryExecutor = queryExecutor;
        }

        public async Task<GetNotAssignedBreweriesResponse> Handle(GetNotAssignedBreweriesRequest request, CancellationToken cancellationToken)
        {
            var query = new GetNotAssignedBreweriesQuery();
            var notAssignedBreweriesFromDb = await _queryExecutor.Execute(query);

            if (notAssignedBreweriesFromDb == null)
            {
                return new GetNotAssignedBreweriesResponse
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetNotAssignedBreweriesResponse
            {
                Data = _mapper.Map<List<Brewery>>(notAssignedBreweriesFromDb)
            };
        }
    }
}
