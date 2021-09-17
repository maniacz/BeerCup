using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Models
{
    public class Vote
    {
        public int VoterId { get; set; }
        public Beer VotedBeer { get; set; }
    }
}
