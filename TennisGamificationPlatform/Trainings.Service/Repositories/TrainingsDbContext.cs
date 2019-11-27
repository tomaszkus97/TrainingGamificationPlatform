using System;
using Microsoft.EntityFrameworkCore;
using Trainings.Service.Domain;

namespace Trainings.Service.Repositories
{
    public class TrainingsDbContext : DbContext
    {
        public TrainingsDbContext(DbContextOptions<TrainingsDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrainingGroup>()
                .Property(p => p.Id)
                .IsRequired();

            modelBuilder.Entity<PlayerTrainingGroup>()
       .HasKey(ptg => new { ptg.TrainingGroupId, ptg.PlayerId });
            modelBuilder.Entity<PlayerTrainingGroup>()
                .HasOne(pg => pg.Player)
                .WithMany(p => p.PlayerTrainingGroups)
                .HasForeignKey(pg => pg.PlayerId);
            modelBuilder.Entity<PlayerTrainingGroup>()
                .HasOne(pg => pg.TrainingGroup)
                .WithMany(g => g.PlayerTrainingGroups)
                .HasForeignKey(pg => pg.TrainingGroupId);

            modelBuilder.Entity<PlayerAttendance>()
       .HasKey(pa => new { pa.AttendanceId, pa.PlayerId });
            modelBuilder.Entity<PlayerAttendance>()
                .HasOne(pa => pa.Player)
                .WithMany(p => p.PlayerAttendances)
                .HasForeignKey(pa => pa.PlayerId);
            modelBuilder.Entity<PlayerAttendance>()
                .HasOne(pa => pa.Attendance)
                .WithMany(a=>a.AttendantPlayers)
                .HasForeignKey(pa => pa.AttendanceId);

            modelBuilder.Entity<Coach>()
                .HasData(new Coach(new Guid(),"Test","Coach"));

            modelBuilder.Entity<Player>()
                .HasData(new Player(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")));

            modelBuilder.Entity<TrainingGroup>()
                .HasData(new TrainingGroup("Wednesday","18:00","Red"));


        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<TrainingGroup> TrainingGroups { get; set; }
        public DbSet<PlayerAttendance> PlayerAttendance { get; set; }
    }
}
