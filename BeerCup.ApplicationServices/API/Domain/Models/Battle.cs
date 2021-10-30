using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain.Models
{
    public class Battle
    {
        public int Id { get; set; }

        public string Style { get; set; }

        public List<Beer> Beers { get; set; }

        public BattlePlace Place { get; set; }

        public bool ResultsPublished { get; set; }

        public DateTime Date { get; set; }

        public string PubName { get; set; }

        public int BattleNo { get; set; }

        public string BattleName { get; set; }
    }
}
