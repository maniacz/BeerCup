using BeerCup.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerCup.DataAccess.Entities
{
    public class AccessCode //: EntityBase
    {
        [Key]
        public int AccessCodeId { get; set; }

        [Required]
        public string Code { get; set; }

        public User User { get; set; }
    }
}
