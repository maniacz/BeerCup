using BeerCup.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeerCup.DataAccess.Entities
{
    public class BattleRouting : EntityBase
    {
        public int FromBattleNo { get; set; }

        public int ToBattleNo { get; set; }

        public bool IsSecondBattle { get; set; }
    }
}
