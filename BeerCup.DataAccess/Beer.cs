using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.DataAccess
{
    public class Beer : EntityBase
    {
        public int BattleId { get; set; }

        public Battle Battle { get; set; }

        public int BreweryId { get; set; }

        public Brewery Brewery { get; set; }

        public int NumberInBattle { get; set; }

        public List<User> VotingUsers { get; set; }
    }
}
