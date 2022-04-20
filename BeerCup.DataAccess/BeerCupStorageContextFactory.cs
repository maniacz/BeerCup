using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BeerCup.DataAccess
{
    public class BeerCupStorageContextFactory : IDesignTimeDbContextFactory<BeerCupStorageContext>
    {
        public BeerCupStorageContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BeerCupStorageContext>();
            //todo: zmień logowanie na autentykację windowsową
            //localDb
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=BeerCupStorage;Persist Security Info=True;User ID=sa;Password=sysadmin1.");
            //Azure
            //todo: Zupdatuj bazę na Azure
            //removed, connection string stored in local file D:\BC - materiały\Wyrzucony connection string z secretami bazy z Azure.cs
            return new BeerCupStorageContext(optionsBuilder.Options);
        }
    }
}
