using Microsoft.EntityFrameworkCore;

namespace HorseSense_AspNetCore.Models
{
    public class HorseSenseContext : DbContext
    {
        public HorseSenseContext(DbContextOptions<HorseSenseContext> options)
            : base(options)
        {
        }

        public DbSet<RaceDay> RaceDays { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Horse> Horses { get; set; }
    }
}