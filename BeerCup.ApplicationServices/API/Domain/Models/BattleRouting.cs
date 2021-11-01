using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain.Models
{
    public class BattleRouting
    {
        public int FromBattleNo { get; set; }

        public int ToBattleNo { get; set; }

        public bool IsSecondBattle { get; set; }
    }
}
