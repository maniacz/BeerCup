using BeerCup.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.DataAccess.Entities
{
    public class BattleResult
    {
        public int FinalRank { get; set; }
        public Brewery Brewery { get; set; }
        public int VotesReceived { get; set; }
        public double Precentage { get; set; }
    }
}
