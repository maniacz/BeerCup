using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess
{
    public class BeerCupStorageContextFactory : IDesignTimeDbContextFactory<BeerCupStorageContext>
    {
        public BeerCupStorageContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BeerCupStorageContext>();
            //todo: zmień logowanie na autentykację windowsową
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=BeerCupStorage;Persist Security Info=True;User ID=sa;Password=sysadmin1.");
            return new BeerCupStorageContext(optionsBuilder.Options);
        }
    }
}
