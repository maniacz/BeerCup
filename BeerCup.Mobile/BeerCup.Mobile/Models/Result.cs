using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Models
{
    public class Result
    {
        public int FinalRank { get; set; }
        public Brewery Brewery { get; set; }
        public int VotesReceived { get; set; }
        public string Precentage { get; set; }
        public int BeerNo { get; set; }
        public bool UserVotedFor { get; set; }

        public Result(int rank, string breweryName, int votes, string precentage)
        {
            FinalRank = rank;
            Brewery = new Brewery { Name = breweryName };
            VotesReceived = votes;
            Precentage = precentage;
        }
    }
}
