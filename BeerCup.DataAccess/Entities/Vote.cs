using BeerCup.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.DataAccess.Entities
{
    public class Vote : EntityBase
    {
        public int BeerId { get; set; }

        public int UserId { get; set; }

        public int BreweryId { get; set; }

        public int BattleId { get; set; }

        public Beer Beer { get; set; }

        public User User { get; set; }
    }
}
