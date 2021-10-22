using BeerCup.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.Entities
{
    public class Battle : EntityBase
    {
        [Required]
        [MaxLength(50)]
        public string Style { get; set; }

        public List<Beer> Beers { get; set; }

        public BattleDate BattleDate { get; set; }

        public bool IsRunning { get; set; }

        public int? BattlePlaceId { get; set; }

        public BattlePlace Place { get; set; }
    }
}
