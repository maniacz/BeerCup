using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.DataAccess.Entities
{
    public class BattleResults
    {
        public string Style { get; set; }

        public List<BattleResult> Results { get; set; }
    }
}
