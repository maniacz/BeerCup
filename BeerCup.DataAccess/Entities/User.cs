using BeerCup.DataAccess.Entities.Base;
using BeerCup.DataAccess.Enums;
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

        [Required]
        public UserRole Role { get; set; }

        public ICollection<Vote> Votes { get; set; }

        public int AccessCodeId { get; set; }

        public AccessCode AccessCode { get; set; }
    }
}
