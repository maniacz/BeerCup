using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Models.DTO
{
    public class BattleDTO
    {
        public int Id { get; set; }

        public string Style { get; set; }

        public List<Beer> Beers { get; set; }
    }
}
