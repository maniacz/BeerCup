using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Queries;
using BeerCup.DataAccess.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Handlers
{
    public class GetBattlesHandler : IRequestHandler<GetBattlesRequest, GetBattlesResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;

        public GetBattlesHandler(IMapper mapper, IQueryExecutor queryExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
        }

        public async Task<GetBattlesResponse> Handle(GetBattlesRequest request, CancellationToken cancellationToken)
        {
            var query = new GetBattlesQuery()
            {
                Style = request.Style
            };

            var battles = await this.queryExecutor.Execute(query);
            var mappedBattle = this.mapper.Map<List<Domain.Models.Battle>>(battles);

            var response = new GetBattlesResponse()
            {
                Data = mappedBattle
            };

            return response;
        }
    }
}
