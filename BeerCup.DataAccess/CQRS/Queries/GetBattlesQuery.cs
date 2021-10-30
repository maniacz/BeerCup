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

        //todo: czy ta metoda nie było by lepiej gdyby była async? - LEPIEJ I TAK JĄ PRZEROBIŁEM, ZOSTAWIAM TODO ŻEBY PAMIĘTAĆ O PRZEROBIENIU TAK RESZTY QUERIES I COMMANDS
        public override async Task<List<Battle>> Execute(BeerCupStorageContext context)
        {
            if (!string.IsNullOrEmpty(Style))
            {
                return await context.Battles.
                        Include(b => b.Beers).
                        Where(b => b.Style == Style).ToListAsync();
            }

            return await context.Battles.OrderBy(b => b.BattleNo).ToListAsync();
        }
    }
}
