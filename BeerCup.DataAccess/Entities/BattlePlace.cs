using BeerCup.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerCup.DataAccess.Entities
{
    public class BattlePlace : EntityBase
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public Battle Battle { get; set; }
    }
}
