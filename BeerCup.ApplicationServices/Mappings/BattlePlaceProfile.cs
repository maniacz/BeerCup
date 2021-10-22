using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.Mappings
{
    public class BattlePlaceProfile : Profile
    {
        public BattlePlaceProfile()
        {
            this.CreateMap<DataAccess.Entities.BattlePlace, BattlePlace>()
                .ForMember(x => x.Latitude, y => y.MapFrom(z => z.Latitude))
                .ForMember(x => x.Longitude, y => y.MapFrom(z => z.Longitude));

            this.CreateMap<BattlePlace, DataAccess.Entities.BattlePlace>()
                .ForMember(x => x.Latitude, y => y.MapFrom(z => z.Latitude))
                .ForMember(x => x.Longitude, y => y.MapFrom(z => z.Longitude));

            this.CreateMap<AddBattlePlaceRequest, DataAccess.Entities.BattlePlace>()
                .ForMember(x => x.Latitude, y => y.MapFrom(z => z.Latitude))
                .ForMember(x => x.Longitude, y => y.MapFrom(z => z.Longitude));
        }
    }
}
