﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Models
{
    public class Result
    {
        public int FinalRank { get; set; }
        public Brewery Brewery { get; set; }
        public int VotesReceived { get; set; }
        public double Precentage { get; set; }

        public Result(int rank, string breweryName, int votes, double precentage)
        {
            FinalRank = rank;
            Brewery = new Brewery { Name = breweryName };
            VotesReceived = votes;
            Precentage = precentage;
        }
    }
}
