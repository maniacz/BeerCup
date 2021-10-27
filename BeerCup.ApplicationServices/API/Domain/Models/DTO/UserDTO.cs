using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain.Models.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public int Role { get; set; }

        public bool IsAuthenticated { get; set; }
    }
}
