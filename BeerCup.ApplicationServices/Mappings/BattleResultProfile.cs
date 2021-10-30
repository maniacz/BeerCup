using AutoMapper;
using BeerCup.ApplicationServices.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.Mappings
{
    public class BattleResultProfile : Profile
    {
        public BattleResultProfile()
        {
            this.CreateMap<DataAccess.Entities.BattleResult, BattleResult>()
                .ForMember(x => x.Brewery, y => y.MapFrom(z => z.Brewery))
                .ForMember(x => x.VotesReceived, y => y.MapFrom(z => z.VotesReceived))
                .ForMember(x => x.BeerNo, y => y.MapFrom(z => z.BeerNo))
                .ForMember(x => x.BeerId, y => y.MapFrom(z => z.BeerId));

            this.CreateMap<BattleResult, DataAccess.Entities.BattleResult>()
                .ForMember(x => x.Brewery, y => y.MapFrom(z => z.Brewery))
                .ForMember(x => x.VotesReceived, y => y.MapFrom(z => z.VotesReceived))
                .ForMember(x => x.BeerNo, y => y.MapFrom(z => z.BeerNo))
                .ForMember(x => x.BeerId, y => y.MapFrom(z => z.BeerId));
        }
    }
}
