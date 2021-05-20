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

            modelBuilder.Entity("BeerCup.DataAccess.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Style")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Battles");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Beer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BattleId")
                        .HasColumnType("int");

                    b.Property<int>("NumberInBattle")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BattleId");

                    b.ToTable("Beers");
                });

            modelBuilder.Entity("BeerCup.DataAccess.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Battle", b =>
                {
                    b.HasOne("BeerCup.DataAccess.User", null)
                        .WithMany("BattlesAttended")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Beer", b =>
                {
                    b.HasOne("BeerCup.DataAccess.Battle", "Battle")
                        .WithMany("Beers")
                        .HasForeignKey("BattleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Battle");
                });

            modelBuilder.Entity("BeerCup.DataAccess.Battle", b =>
                {
                    b.Navigation("Beers");
                });

            modelBuilder.Entity("BeerCup.DataAccess.User", b =>
                {
                    b.Navigation("BattlesAttended");
                });
#pragma warning restore 612, 618
        }
    }
}
