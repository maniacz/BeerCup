using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Models
{
    public class Battle
    {
        public int Id { get; set; }

        public string Style { get; set; }

        public List<Beer> Beers { get; set; }

        public BattlePlace Place { get; set; }

        public bool ResultsPublished { get; set; }

        public DateTime? Date { get; set; }

        public string PubName { get; set; }
    }
}
