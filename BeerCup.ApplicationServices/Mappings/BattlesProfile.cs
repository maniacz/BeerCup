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
    public class BattlesProfile : Profile
    {
        public BattlesProfile()
        {
            this.CreateMap<DataAccess.Entities.Battle, Battle>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Style, y => y.MapFrom(z => z.Style))
                .ForMember(x => x.Beers, y => y.MapFrom(z => z.Beers != null ? z.Beers : new List<DataAccess.Entities.Beer>()))
                .ForMember(x => x.Place, y => y.MapFrom(z => z.Place))
                .ForMember(x => x.ResultsPublished, y => y.MapFrom(z => z.ResultsPublished))
                .ForMember(x => x.Date, y => y.MapFrom(z => z.Date))
                .ForMember(x => x.PubName, y => y.MapFrom(z => z.PubName));

            this.CreateMap<Battle, DataAccess.Entities.Battle>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Style, y => y.MapFrom(z => z.Style))
                .ForMember(x => x.Beers, y => y.MapFrom(z => z.Beers != null ? z.Beers : new List<Beer>()))
                .ForMember(x => x.Place, y => y.MapFrom(z => z.Place))
                .ForMember(x => x.ResultsPublished, y => y.MapFrom(z => z.ResultsPublished))
                .ForMember(x => x.Date, y => y.MapFrom(z => z.Date))
                .ForMember(x => x.PubName, y => y.MapFrom(z => z.PubName));

            this.CreateMap<AddNewBattleRequest, DataAccess.Entities.Battle>()
                .ForMember(x => x.BattleNo, y => y.MapFrom(z => z.BattleNo))
                .ForMember(x => x.BattleName, y => y.MapFrom(z => z.BattleName))
                .ForMember(x => x.Style, y => y.MapFrom(z => z.Style))
                .ForMember(x => x.PubName, y => y.MapFrom(z => z.PubName))
                .ForMember(x => x.Date, y => y.MapFrom(z => z.Date));
        }
    }
}
