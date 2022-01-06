using BeerCup.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.DataAccess.Entities
{
    public class LuckyVoter : EntityBase
    {
        public int BattleId { get; set; }

        public Battle Battle { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
