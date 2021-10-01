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
    public class VoteProfile : Profile
    {
        public VoteProfile()
        {
            this.CreateMap<DataAccess.Entities.Vote, Vote>()
                .ForMember(x => x.BeerId, y => y.MapFrom(z => z.BeerId))
                .ForMember(x => x.VoterId, y => y.MapFrom(z => z.UserId));

            this.CreateMap<SendVoteRequest, DataAccess.Entities.Vote>()
                .ForMember(x => x.BeerId, y => y.MapFrom(z => z.BeerId))
                .ForMember(x => x.UserId, y => y.MapFrom(z => z.VoterId));

            this.CreateMap<Vote, DataAccess.Entities.Vote>()
                .ForMember(x => x.BeerId, y => y.MapFrom(z => z.BeerId))
                .ForMember(x => x.UserId, y => y.MapFrom(z => z.VoterId));
        }
    }
}
