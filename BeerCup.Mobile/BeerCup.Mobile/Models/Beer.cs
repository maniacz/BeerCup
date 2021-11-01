using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Models
{
    public class Beer
    {
        public int BeerId { get; set; }

        public int AssignedNumberInBattle { get; set; }

        public int BreweryId { get; set; }

        public Brewery BrewedBy { get; set; }

        public int BattleId { get; set; }

        public Battle Battle { get; set; }

        public bool SelectedByVoter { get; set; }
    }
}
