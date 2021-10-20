using BeerCup.ApplicationServices.API.Domain.Models;
using BeerCup.ApplicationServices.API.Domain.Models.DTO;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class CreateUserResponse : ResponseBase<UserDTO>
    {
        public bool IsAuthenticated { get; set; }
    }
}