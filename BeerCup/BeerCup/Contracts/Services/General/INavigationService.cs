using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Contracts.Services.General
{
    public interface INavigationService
    {
        Task InitializeAsync();
    }
}
