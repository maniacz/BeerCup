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
            modelBuilder.Entity<Battle>().Property(p => p.BattleNo).HasDefaultValue(0);
            modelBuilder.Entity<Battle>().Property(p => p.BattleName).HasDefaultValue("DefaultName");
            modelBuilder.Entity<BattleRouting>().HasData(_battleRoutings);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Battle> Battles { get; set; }

        public DbSet<Beer> Beers { get; set; }

        public DbSet<Brewery> Breweries { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<AccessCode> AccessCodes { get; set; }

        public DbSet<BattlePlace> BattlePlaces { get; set; }

        public DbSet<BattleRouting> BattleRoutings { get; set; }

        public DbSet<LuckyVoter> LuckyVoters { get; set; }

        private AccessCode[] _defaultAccessCodes = new AccessCode[]
        {
            new AccessCode { AccessCodeId = 1, Code = "A001" },
            new AccessCode { AccessCodeId = 2, Code = "A002" },
            new AccessCode { AccessCodeId = 3, Code = "A003" },
            new AccessCode { AccessCodeId = 4, Code = "A004" },
            new AccessCode { AccessCodeId = 5, Code = "V001" },
            new AccessCode { AccessCodeId = 6, Code = "V002" },
        };
        private BattleRouting[] _battleRoutings = new BattleRouting[]
        {
            new BattleRouting {Id = 1, FromBattleNo = 1, ToBattleNo = 9, IsSecondBattle = false},
            new BattleRouting {Id = 2, FromBattleNo = 2, ToBattleNo = 9, IsSecondBattle = true},
            new BattleRouting {Id = 3, FromBattleNo = 3, ToBattleNo = 10, IsSecondBattle = false},
            new BattleRouting {Id = 4, FromBattleNo = 4, ToBattleNo = 10, IsSecondBattle = true},
            new BattleRouting {Id = 5, FromBattleNo = 5, ToBattleNo = 11, IsSecondBattle = false},
            new BattleRouting {Id = 6, FromBattleNo = 6, ToBattleNo = 11, IsSecondBattle = true},
            new BattleRouting {Id = 7, FromBattleNo = 7, ToBattleNo = 12, IsSecondBattle = false},
            new BattleRouting {Id = 8, FromBattleNo = 8, ToBattleNo = 12, IsSecondBattle = true},

            new BattleRouting {Id = 9, FromBattleNo = 9, ToBattleNo = 13, IsSecondBattle = false},
            new BattleRouting {Id = 10, FromBattleNo = 10, ToBattleNo = 13, IsSecondBattle = true},
            new BattleRouting {Id = 11, FromBattleNo = 11, ToBattleNo = 14, IsSecondBattle = false},
            new BattleRouting {Id = 12, FromBattleNo = 12, ToBattleNo = 14, IsSecondBattle = true},

            new BattleRouting {Id = 13, FromBattleNo = 13, ToBattleNo = 15, IsSecondBattle = false},
            new BattleRouting {Id = 14, FromBattleNo = 14, ToBattleNo = 15, IsSecondBattle = true},
        };
    }
}
