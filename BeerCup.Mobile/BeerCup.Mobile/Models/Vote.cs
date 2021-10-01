using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Models
{
    public class Vote
    {
        public int VoterId { get; set; }
        public int BeerId { get; set; }
        public int BattleId { get; set; }
    }
}
