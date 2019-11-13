﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Players.Service.Repositories;

namespace Players.Service.Migrations
{
    [DbContext(typeof(PlayersDbContext))]
    [Migration("20191110121903_first")]
    partial class first
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Players.Service.Domain.Level", b =>
                {
                    b.Property<int>("LevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.Property<int>("PointsToAdvance")
                        .HasColumnType("int");

                    b.HasKey("LevelId");

                    b.ToTable("Levels");

                    b.HasData(
                        new
                        {
                            LevelId = 1,
                            Name = 0,
                            PointsToAdvance = 250
                        });
                });

            modelBuilder.Entity("Players.Service.Domain.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<Guid>("IdentityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            Id = new Guid("44420a8f-ec2c-4ad1-aa2c-57a8624c7b3f"),
                            Age = 8,
                            IdentityId = new Guid("3cb57a49-a626-41d6-5f19-08d760866da9"),
                            LevelId = 1,
                            Name = "Player",
                            Points = 0,
                            Surname = "One"
                        });
                });

            modelBuilder.Entity("Players.Service.Domain.Player", b =>
                {
                    b.HasOne("Players.Service.Domain.Level", "CurrentLevel")
                        .WithMany()
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
