﻿using BeerCup.ApplicationServices.API.Domain;
using BeerCup.DataAccess;
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
        private readonly IRepository<Battle> repository;

        public GetBattlesHandler(IRepository<DataAccess.Entities.Battle> repository)
        {
            this.repository = repository;
        }

        public Task<GetBattlesResponse> Handle(GetBattlesRequest request, CancellationToken cancellationToken)
        {
            var battles = this.repository.GetAll();
            var domainBattles = battles.Select(b => new Domain.Models.Battle()
            {
                Id = b.Id,
                Style = b.Style
            });

            var response = new GetBattlesResponse()
            {
                Data = domainBattles.ToList()
            };

            return Task.FromResult(response);
        }
    }
}
