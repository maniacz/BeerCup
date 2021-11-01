using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeerCup.Mobile.Models
{
    public class Result
    {
        public int FinalRank { get; set; }
        public Brewery Brewery { get; set; }
        public int VotesReceived { get; set; }
        public decimal Precentage { get; set; }
        public int BeerNo { get; set; }
        public int BeerId { get; set; }
        public bool UserVotedFor { get; set; }

        public Result(int rank, string breweryName, int votes, decimal precentage)
        {
            FinalRank = rank;
            Brewery = new Brewery { Name = breweryName };
            VotesReceived = votes;
            Precentage = precentage;
        }
    }
}
