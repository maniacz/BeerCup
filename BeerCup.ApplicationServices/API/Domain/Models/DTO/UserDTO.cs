using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain.Models.DTO
{
    public class UserDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public int Role { get; set; }

        public string Salt { get; set; }

        public int AccessCodeId { get; set; }

    }
}
