using BeerCup.DataAccess.Entities;
using BeerCup.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess
{
    public class BeerCupStorageContext : DbContext
    {
        public BeerCupStorageContext(DbContextOptions<BeerCupStorageContext> options) : base(options)
        {
        }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddDebug(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=BeerCupStorage;Persist Security Info=True;User ID=sa;Password=sysadmin1.");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().Property(p => p.Role).HasDefaultValue(UserRole.Voter);
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            //modelBuilder.Entity<User>().HasOne(u => u.AccessCode).WithOne(c => c.User).HasForeignKey<AccessCode>(c => c.Id);
            modelBuilder.Entity<Vote>().HasIndex(v => new { v.UserId, v.BeerId, v.BreweryId }).IsUnique();
            modelBuilder.Entity<AccessCode>().HasData(_defaultAccessCodes);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Battle> Battles { get; set; }

        public DbSet<Beer> Beers { get; set; }

        public DbSet<Brewery> Breweries { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<AccessCode> AccessCodes { get; set; }

        public DbSet<BattleDate> BattleDates { get; set; }

        private AccessCode[] _defaultAccessCodes = new AccessCode[]
        {
            new AccessCode { AccessCodeId = 1, Code = "A001" },
            new AccessCode { AccessCodeId = 2, Code = "A002" },
            new AccessCode { AccessCodeId = 3, Code = "A003" },
            new AccessCode { AccessCodeId = 4, Code = "A004" },
            new AccessCode { AccessCodeId = 5, Code = "V001" },
            new AccessCode { AccessCodeId = 6, Code = "V002" },
        };
    }
}
