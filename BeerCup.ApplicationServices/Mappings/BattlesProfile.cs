using AutoMapper;
using BeerCup.ApplicationServices.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.Mappings
{
    public class BattlesProfile : Profile
    {
        public BattlesProfile()
        {
            this.CreateMap<DataAccess.Entities.Battle, Battle>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Style, y => y.MapFrom(z => z.Style));

            this.CreateMap<Battle, DataAccess.Entities.Battle>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Style, y => y.MapFrom(z => z.Style));
        }
    }
}
