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
    public class BreweryProfile : Profile
    {
        public BreweryProfile()
        {
            this.CreateMap<AddBreweryRequest, DataAccess.Entities.Brewery>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Id, y => y.MapFrom(z => z.BreweryId));

            this.CreateMap<DataAccess.Entities.Brewery, Brewery>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.BreweryId, y => y.MapFrom(z => z.Id));
        }
    }
}
