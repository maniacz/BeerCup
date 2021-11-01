using AutoMapper;
using BeerCup.ApplicationServices.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.Mappings
{
    public class RoutingsProfile : Profile
    {
        public RoutingsProfile()
        {
            this.CreateMap<DataAccess.Entities.BattleRouting, BattleRouting>();
        }
    }
}
