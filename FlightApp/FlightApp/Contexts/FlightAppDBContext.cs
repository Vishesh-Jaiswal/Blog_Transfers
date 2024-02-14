using FlightApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightApp.Contexts
{
    public class FlightAppDBContext:DbContext
    {
        public FlightAppDBContext(DbContextOptions options) : base(options)
        {
            Users=Set<User>();
            Flights = Set<Flight>();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.flights)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserEmail);

            base.OnModelCreating(modelBuilder);
        }
    }
}
