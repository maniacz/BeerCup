using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
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
    public class GetBeerByIdHandler : IRequestHandler<GetBeerByIdRequest, GetBeerByIdResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;

        public GetBeerByIdHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetBeerByIdResponse> Handle(GetBeerByIdRequest request, CancellationToken cancellationToken)
        {
            var query = new GetBeerQuery()
            {
                Id = request.BeerId
            };
            
            var beer = await queryExecutor.Execute(query);
            var mappedBeer = this.mapper.Map<Domain.Models.Beer>(beer);

            var response = new GetBeerByIdResponse()
            {
                Data = mappedBeer
            };

            return response;
        }
    }
}
