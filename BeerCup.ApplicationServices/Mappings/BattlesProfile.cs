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
                .ForMember(x => x.Style, y => y.MapFrom(z => z.Style))
                .ForMember(x => x.Beers, y => y.MapFrom(z => z.Beers != null ? z.Beers : new List<DataAccess.Entities.Beer>()))
                .ForMember(x => x.Place, y => y.MapFrom(z => z.Place))
                .ForMember(x => x.ResultsPublished, y => y.MapFrom(z => z.ResultsPublished));

            this.CreateMap<Battle, DataAccess.Entities.Battle>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Style, y => y.MapFrom(z => z.Style))
                .ForMember(x => x.Beers, y => y.MapFrom(z => z.Beers != null ? z.Beers : new List<Beer>()))
                .ForMember(x => x.Place, y => y.MapFrom(z => z.Place))
                .ForMember(x => x.ResultsPublished, y => y.MapFrom(z => z.ResultsPublished));
        }
    }
}
