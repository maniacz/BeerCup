using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain.Models
{
    public class BattleResults
    {
        public string Style { get; set; }

        public List<BattleResult> Results { get; set; }
    }
}
