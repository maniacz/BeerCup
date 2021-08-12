using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerCup.DataAccess.Entities
{
    public class User : EntityBase
    {
        [Required]
        [MaxLength(25)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MaxLength(24)]
        public string Salt { get; set; }

        public List<Beer> VotedBeers { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
