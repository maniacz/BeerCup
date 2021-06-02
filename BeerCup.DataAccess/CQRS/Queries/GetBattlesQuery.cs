using BeerCup.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess.CQRS.Queries
{
    public class GetBattlesQuery : QueryBase<List<Battle>>
    {
        public string Style { get; set; }

        //todo: czy ta metoda nie było by lepiej gdyby była async?
        public override Task<List<Battle>> Execute(BeerCupStorageContext context)
        {
            if (!string.IsNullOrEmpty(Style))
            {
                return context.Battles.
                        Include(b => b.Beers).
                        Where(b => b.Style == Style).ToListAsync();
            }

            return context.Battles.ToListAsync();
        }
    }
}
