using BeerCup.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.DataAccess.Entities
{
    public class Beer : EntityBase
    {
        public int BattleId { get; set; }

        public Battle Battle { get; set; }

        public int BreweryId { get; set; }

        public Brewery Brewery { get; set; }

        public int NumberInBattle { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
