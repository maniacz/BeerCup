using AutoMapper;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.Models.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Mappings
{
    public class BattlePlaceProfile : Profile
    {
        public BattlePlaceProfile()
        {
            CreateMap<BattlePlaceResponse, BattlePlace>()
                .ForMember(x => x.Latitude, y => y.MapFrom(z => z.Data.Latitude))
                .ForMember(x => x.Longitude, y => y.MapFrom(z => z.Data.Longitude));
        }
    }
}
