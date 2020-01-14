using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamification.Service.Domain;
using Microsoft.EntityFrameworkCore;

namespace Gamification.Service.Repositories
{
    public class GamificationDbContext : DbContext
    {
        public GamificationDbContext(DbContextOptions<GamificationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Challenge>()
                .Property(c => c.Id)
                .IsRequired();
        }

        public DbSet<Challenge> Challenges { get; set; }
    }
}
