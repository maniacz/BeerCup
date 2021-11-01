﻿// <auto-generated />
using System;
using BeerCup.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BeerCup.DataAccess.Migrations
{
    [DbContext(typeof(BeerCupStorageContext))]
    partial class BeerCupStorageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("BattleName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasDefaultValue("DefaultName");

                    b.Property<int>("BattleNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int?>("BattlePlaceId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRunning")
                        .HasColumnType("bit");

                    b.Property<string>("PubName")
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("BeerCup.DataAccess.Entities.BattleRouting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FromBattleNo")
                        .HasColumnType("int");

                    b.Property<bool>("IsSecondBattle")
                        .HasColumnType("bit");

                    b.Property<int>("ToBattleNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BattleRoutings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FromBattleNo = 1,
                            IsSecondBattle = false,
                            ToBattleNo = 9
                        },
                        new
                        {
                            Id = 2,
                            FromBattleNo = 2,
                            IsSecondBattle = true,
                            ToBattleNo = 9
                        },
                        new
                        {
                            Id = 3,
                            FromBattleNo = 3,
                            IsSecondBattle = false,
                            ToBattleNo = 10
                        },
                        new
                        {
                            Id = 4,
                            FromBattleNo = 4,
                            IsSecondBattle = true,
                            ToBattleNo = 10
                        },
                        new
                        {
                            Id = 5,
                            FromBattleNo = 5,
                            IsSecondBattle = false,
                            ToBattleNo = 11
                        },
                        new
                        {
                            Id = 6,
                            FromBattleNo = 6,
                            IsSecondBattle = true,
                            ToBattleNo = 11
                        },
                        new
                        {
                            Id = 7,
                            FromBattleNo = 7,
                            IsSecondBattle = false,
                            ToBattleNo = 12
                        },
                        new
                        {
                            Id = 8,
                            FromBattleNo = 8,
                            IsSecondBattle = true,
                            ToBattleNo = 12
                        },
                        new
                        {
                            Id = 9,
                            FromBattleNo = 9,
                            IsSecondBattle = false,
                            ToBattleNo = 13
                        },
                        new
                        {
                            Id = 10,
                            FromBattleNo = 10,
                            IsSecondBattle = true,
                            ToBattleNo = 13
                        },
                        new
                        {
                            Id = 11,
                            FromBattleNo = 11,
                            IsSecondBattle = false,
                            ToBattleNo = 14
                        },
                        new
                        {
                            Id = 12,
                            FromBattleNo = 12,
                            IsSecondBattle = true,
                            ToBattleNo = 14
                        },
                        new
                        {
                            Id = 13,
                            FromBattleNo = 13,
                            IsSecondBattle = false,
                            ToBattleNo = 15
                        },
                        new
                        {
                            Id = 14,
                            FromBattleNo = 14,
                            IsSecondBattle = true,
                            ToBattleNo = 15
                        });
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

                    b.Property<int?>("NumberInBattle")
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
