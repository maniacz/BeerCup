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
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<DataAccess.Entities.User, User>()
                .ForMember(x => x.Username, y => y.MapFrom(z => z.Username))
                .ForMember(x => x.Password, y => y.MapFrom(z => z.Password));

            this.CreateMap<CreateUserRequest, DataAccess.Entities.User>()
                .ForMember(x => x.Username, y => y.MapFrom(z => z.Username));
                //.ForMember(x => x.Password, y => y.MapFrom(z => z.Password));
        }
    }
}
