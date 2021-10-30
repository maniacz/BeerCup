using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain.Models
{
    public class Beer
    {
        public int BeerId { get; set; }

        public int BattleId { get; set; }

        public Brewery BrewedBy { get; set; }
    }
}
