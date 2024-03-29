﻿using AutoMapper;
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
    public class GetBattleByNoHandler : IRequestHandler<GetBattleByNoRequest, GetBattleByNoResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;

        public GetBattleByNoHandler(IMapper mapper, IQueryExecutor queryExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
        }

        public async Task<GetBattleByNoResponse> Handle(GetBattleByNoRequest request, CancellationToken cancellationToken)
        {
            var query = new GetBattleByNoQuery()
            {
                BattleNo = request.BattleId
            };

            var battle = await this.queryExecutor.Execute(query);
            if (battle == null)
            {
                return new GetBattleByNoResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var mappedBattle = this.mapper.Map<Domain.Models.Battle>(battle);
            var response = new GetBattleByNoResponse()
            {
                Data = mappedBattle
            };

            return response;
        }
    }
}
