using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerCup.DataAccess.Entities
{
    public class Brewery : EntityBase
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Beer> Beers { get; set; }
    }
}
