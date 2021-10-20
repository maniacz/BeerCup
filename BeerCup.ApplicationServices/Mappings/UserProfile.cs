using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
using BeerCup.ApplicationServices.API.Domain.Models.DAO;
using BeerCup.ApplicationServices.API.Domain.Models.DTO;
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
                .ForMember(x => x.Password, y => y.MapFrom(z => z.Password))
                .ForMember(x => x.UserId, y => y.MapFrom(z => z.Id));

            this.CreateMap<CreateUserRequest, DataAccess.Entities.User>()
                .ForMember(x => x.Username, y => y.MapFrom(z => z.Username));
            //.ForMember(x => x.Password, y => y.MapFrom(z => z.Password));

            this.CreateMap<CreateUserRequest, UserDAO>()
                .ForMember(x => x.Username, y => y.MapFrom(z => z.Username))
                .ForMember(x => x.Password, y => y.MapFrom(z => z.Password));

            this.CreateMap<UserDAO, DataAccess.Entities.User>();

            this.CreateMap<DataAccess.Entities.User, UserDTO>()
                .ForMember(x => x.Username, y => y.MapFrom(z => z.Username))
                .ForMember(x => x.Role, y => y.MapFrom(z => z.Role))
                .ForMember(x => x.UserId, y => y.MapFrom(z => z.Id));
        }
    }
}
