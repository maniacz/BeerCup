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
    public class GetBattleByIdHandler : IRequestHandler<GetBattleByIdRequest, GetBattleByIdResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;

        public GetBattleByIdHandler(IMapper mapper, IQueryExecutor queryExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
        }

        public async Task<GetBattleByIdResponse> Handle(GetBattleByIdRequest request, CancellationToken cancellationToken)
        {
            var query = new GetBattleQuery()
            {
                Id = request.BattleId
            };

            var battle = await this.queryExecutor.Execute(query);
            var mappedBattle = this.mapper.Map<Domain.Models.Battle>(battle);

            var response = new GetBattleByIdResponse()
            {
                Data = mappedBattle
            };

            return response;
        }
    }
}
