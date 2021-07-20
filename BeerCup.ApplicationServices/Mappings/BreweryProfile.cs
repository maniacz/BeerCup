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
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name));

            this.CreateMap<DataAccess.Entities.Brewery, Brewery>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name));
        }
    }
}
