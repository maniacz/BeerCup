using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain
{
    public abstract class RequestBase
    {
        public string RequestUsername { get; set; }
    }
}
