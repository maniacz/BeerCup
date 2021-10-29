﻿using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Contracts.Services.Data
{
    public interface IVotingDataService
    {
        Task<IEnumerable<Vote>> SendVotes(IEnumerable<Beer> votes);
    }
}
