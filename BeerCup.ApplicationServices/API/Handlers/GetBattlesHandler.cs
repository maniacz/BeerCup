using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
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
        private readonly IMapper mapper;

        public GetBattlesHandler(IRepository<DataAccess.Entities.Battle> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<GetBattlesResponse> Handle(GetBattlesRequest request, CancellationToken cancellationToken)
        {
            var battles = await this.repository.GetAll();
            var mappedBattle = this.mapper.Map<List<Domain.Models.Battle>>(battles);


            //var domainBattles = battles.Select(b => new Domain.Models.Battle()
            //{
            //    Id = b.Id,
            //    Style = b.Style
            //});

            var response = new GetBattlesResponse()
            {
                //Data = domainBattles.ToList()
                Data = mappedBattle
            };

            //return Task.FromResult(response);
            return response;
        }
    }
}
