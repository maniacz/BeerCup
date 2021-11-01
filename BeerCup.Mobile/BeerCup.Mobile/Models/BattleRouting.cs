using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Models
{
    public class BattleRouting
    {
        public int FromBattleNo { get; set; }

        public int ToBattleNo { get; set; }

        public bool IsSecondBattle { get; set; }
    }
}

