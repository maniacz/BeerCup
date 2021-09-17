using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain.Models
{
    public class Vote
    {
        public int VoterId { get; set; }

        public int BeerId { get; set; }
    }
}
