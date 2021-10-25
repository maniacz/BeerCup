using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain.Models
{
    public class BattleResult
    {
        public Brewery Brewery { get; set; }
        public int VotesReceived { get; set; }
    }
}
