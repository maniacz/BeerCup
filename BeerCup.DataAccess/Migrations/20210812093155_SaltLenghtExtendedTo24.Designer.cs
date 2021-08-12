﻿// <auto-generated />
using BeerCup.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BeerCup.DataAccess.Migrations
{
    [DbContext(typeof(BeerCupStorageContext))]
    [Migration("20210812093155_SaltLenghtExtendedTo24")]
    partial class SaltLenghtExtendedTo24
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BeerCup.DataAccess.Entities.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Style")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Battles");
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

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(2);

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BeerUser", b =>
                {
                    b.Property<int>("VotedBeersId")
                        .HasColumnType("int");

                    b.Property<int>("VotingUsersId")
                        .HasColumnType("int");

                    b.HasKey("VotedBeersId", "VotingUsersId");

                    b.HasIndex("VotingUsersId");

                    b.ToTable("BeerUser");
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

            modelBuilder.Entity("BeerUser", b =>
                {
                    b.HasOne("BeerCup.DataAccess.Entities.Beer", null)
                        .WithMany()
                        .HasForeignKey("VotedBeersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeerCup.DataAccess.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("VotingUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.Battle", b =>
                {
                    b.Navigation("Beers");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Entities.Brewery", b =>
                {
                    b.Navigation("Beers");
                });
#pragma warning restore 612, 618
        }
    }
}
