using AutoMapper;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Mappings
{
    public class BeersProfile : Profile
    {
        public BeersProfile()
        {
            CreateMap<BeerFromBattleResponse, Beer>()
                .ForMember(x => x.BeerId, y => y.MapFrom(z => z.Data.BeerId))
                .ForPath(x => x.Battle.BattleId, y => y.MapFrom(z => z.Data.BattleId));
        }
    }
}
