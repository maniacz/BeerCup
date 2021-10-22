using AutoMapper;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Mappings
{
    public class BattleProfile : Profile
    {
        public BattleProfile()
        {
            CreateMap<BattleResponse, Battle>()
                .ForMember(x => x.BattleId, y => y.Condition(r => r.Data != null))
                .ForMember(x => x.BattleId, y => y.MapFrom(z => z.Data.Id));
        }
    }
}
