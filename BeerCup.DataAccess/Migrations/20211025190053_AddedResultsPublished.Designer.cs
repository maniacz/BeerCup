﻿// <auto-generated />
using System;
using BeerCup.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BeerCup.DataAccess.Migrations
{
    [DbContext(typeof(BeerCupStorageContext))]
    [Migration("20211025190053_AddedResultsPublished")]
    partial class AddedResultsPublished
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BeerCup.DataAccess.Entities.AccessCode", b =>
                {
                    b.Property<int>("AccessCodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccessCodeId");

                    b.ToTable("AccessCodes");

                    b.HasData(
                        new
                        {
                            AccessCodeId = 1,
                            Code = "A001"
                        },
                        new
                        {
                            AccessCodeId = 2,
                            Code = "A002"
                        },
                        new
                        {
                            AccessCodeId = 3,
                            Code = "A003"
                        },
                        new
                        {
                            AccessCodeId = 4,
                            Code = "A004"
                        },
                        new
                        {
                            AccessCodeId = 5,
                            Code = "V001"
                        },
                        new
                        {
                            AccessCodeId = 6,
                            Code = "V002"
                        });
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BattlePlaceId")
                        .HasColumnType("int");

                    b.Property<bool>("IsRunning")
                        .HasColumnType("bit");

                    b.Property<bool>("ResultsPublished")
                        .HasColumnType("bit");

                    b.Property<string>("Style")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("BattlePlaceId")
                        .IsUnique()
                        .HasFilter("[BattlePlaceId] IS NOT NULL");

                    b.ToTable("Battles");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.BattleDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BattleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BattleId")
                        .IsUnique();

                    b.ToTable("BattleDates");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.BattlePlace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("BattlePlaces");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.Beer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BattleId")
                        .HasColumnType("int");

                    b.Property<int>("BreweryId")
                        .HasColumnType("int");

                    b.Property<int>("NumberInBattle")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BattleId");

                    b.HasIndex("BreweryId");

                    b.ToTable("Beers");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.Brewery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Breweries");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessCodeId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(3);

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("AccessCodeId")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BattleId")
                        .HasColumnType("int");

                    b.Property<int>("BeerId")
                        .HasColumnType("int");

                    b.Property<int>("BreweryId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("UserId", "BeerId", "BreweryId")
                        .IsUnique();

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.Battle", b =>
                {
                    b.HasOne("BeerCup.DataAccess.Entities.BattlePlace", "Place")
                        .WithOne("Battle")
                        .HasForeignKey("BeerCup.DataAccess.Entities.Battle", "BattlePlaceId");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.BattleDate", b =>
                {
                    b.HasOne("BeerCup.DataAccess.Entities.Battle", "Battle")
                        .WithOne("BattleDate")
                        .HasForeignKey("BeerCup.DataAccess.Entities.BattleDate", "BattleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Battle");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.Beer", b =>
                {
                    b.HasOne("BeerCup.DataAccess.Entities.Battle", "Battle")
                        .WithMany("Beers")
                        .HasForeignKey("BattleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeerCup.DataAccess.Entities.Brewery", "Brewery")
                        .WithMany("Beers")
                        .HasForeignKey("BreweryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Battle");

                    b.Navigation("Brewery");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.User", b =>
                {
                    b.HasOne("BeerCup.DataAccess.Entities.AccessCode", "AccessCode")
                        .WithOne("User")
                        .HasForeignKey("BeerCup.DataAccess.Entities.User", "AccessCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccessCode");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.Vote", b =>
                {
                    b.HasOne("BeerCup.DataAccess.Entities.Beer", "Beer")
                        .WithMany("Votes")
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeerCup.DataAccess.Entities.User", "User")
                        .WithMany("Votes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.AccessCode", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.Battle", b =>
                {
                    b.Navigation("BattleDate");

                    b.Navigation("Beers");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.BattlePlace", b =>
                {
                    b.Navigation("Battle");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.Beer", b =>
                {
                    b.Navigation("Votes");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.Brewery", b =>
                {
                    b.Navigation("Beers");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.User", b =>
                {
                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
