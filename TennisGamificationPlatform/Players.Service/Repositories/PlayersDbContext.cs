using System;
using Microsoft.EntityFrameworkCore;
using Players.Service.Contracts;
using Players.Service.Domain;

namespace Players.Service.Repositories
{
    public class PlayersDbContext : DbContext
    {
        public PlayersDbContext(DbContextOptions<PlayersDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .Property(p => p.Id)
                .IsRequired();

            modelBuilder.Entity<Player>()
                .HasOne(p => p.CurrentLevel)
                .WithMany()
                .HasForeignKey(p => p.LevelId);

            modelBuilder.Entity<Level>()
                .HasData(new Level(
                    1, LevelName.Red, 250));

            modelBuilder.Entity<Player>()
                .HasData(new Player(
                    new Guid("44420a8f-ec2c-4ad1-aa2c-57a8624c7b3f"),
                    new Guid("3CB57A49-A626-41D6-5F19-08D760866DA9"),
                    "Player", "One", 8));

            
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<PlayerGroup> PlayersGroups { get; set; }
    }
}
