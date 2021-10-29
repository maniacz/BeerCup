using BeerCup.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.DataAccess.Entities
{
    public class BattleResult
    {
        public Brewery Brewery { get; set; }
        public int VotesReceived { get; set; }
        public int BeerNo { get; set; }
    }
}
