using BeerCup.DataAccess.Entities;
using BeerCup.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().Property(p => p.Role).HasDefaultValue(UserRole.Voter);
            modelBuilder.Entity<Vote>().HasIndex(v => new { v.UserId, v.BeerId, v.BreweryId }).IsUnique();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Battle> Battles { get; set; }

        public DbSet<Beer> Beers { get; set; }

        public DbSet<Brewery> Breweries { get; set; }

        public DbSet<Vote> Votes { get; set; }
    }
}
