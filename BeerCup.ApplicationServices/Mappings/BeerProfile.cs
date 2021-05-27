﻿using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.Mappings
{
    public class BeerProfile : Profile
    {
        public BeerProfile()
        {
            this.CreateMap<AddBeerRequest, DataAccess.Entities.Beer>()
                .ForMember(x => x.BattleId, y => y.MapFrom(z => z.BattleId))
                .ForMember(x => x.BreweryId, y => y.MapFrom(z => z.BreweryId));

            this.CreateMap<DataAccess.Entities.Beer, Beer>()
                .ForMember(x => x.BeerId, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.BattleId, y => y.MapFrom(z => z.BattleId));

            this.CreateMap<RemoveBeerRequest, DataAccess.Entities.Beer>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.BeerId));
        }
    }
}