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
            //localDb
            //optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=BeerCupStorage;Persist Security Info=True;User ID=sa;Password=sysadmin1.");
            //Azure
            optionsBuilder.UseSqlServer("Server=tcp:beercup-db.database.windows.net,1433;Initial Catalog=BeerCupStorage;Persist Security Info=False;User ID=lukasz;Password=ONxPFt4AMKH0WfXa;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            return new BeerCupStorageContext(optionsBuilder.Options);
        }
    }
}
