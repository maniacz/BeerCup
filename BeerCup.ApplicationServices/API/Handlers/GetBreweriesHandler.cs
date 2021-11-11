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
    public class GetBreweriesHandler : IRequestHandler<GetBreweriesRequest, GetBreweriesResponse>
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public GetBreweriesHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<GetBreweriesResponse> Handle(GetBreweriesRequest request, CancellationToken cancellationToken)
        {
            var query = new GetBreweriesQuery();

            var breweriesFromDb = await _queryExecutor.Execute(query);
            if (breweriesFromDb == null)
            {
                return new GetBreweriesResponse
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetBreweriesResponse
            {
                Data = _mapper.Map<List<Brewery>>(breweriesFromDb)
            };
        }
    }
}
