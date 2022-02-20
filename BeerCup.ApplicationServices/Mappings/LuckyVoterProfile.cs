using AutoMapper;
using BeerCup.ApplicationServices.API.Domain.Models;

namespace BeerCup.ApplicationServices.Mappings
{
    public class LuckyVoterProfile : Profile
    {
        public LuckyVoterProfile()
        {
            this.CreateMap<DataAccess.Entities.LuckyVoter, LuckyVoter>()
                .ForMember(x => x.Username, y => y.MapFrom(z => z.User.Username))
                .ForMember(x => x.VoterId, y => y.MapFrom(z => z.UserId))
                .ForMember(x => x.BattleId, y => y.MapFrom(z => z.BattleId))
                .ForMember(x => x.BattleStyle, y => y.MapFrom(z => z.Battle.Style))
                .ForMember(x => x.IsPaperVote, y => y.MapFrom(z => z.IsPaperVote));
        }
    }
}
