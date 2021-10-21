using BeerCup.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.DataAccess.Entities
{
    public class BattleDate : EntityBase
    {
        public DateTime Date { get; set; }

        public int BattleId { get; set; }

        public Battle Battle { get; set; }
    }
}
