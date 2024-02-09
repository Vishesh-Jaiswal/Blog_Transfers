using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Models;

namespace OnlineBookStore.Contexts
{
    public class OnlineBookAppContext:DbContext
    {
        public OnlineBookAppContext(DbContextOptions options) : base(options)
        {
            Users = Set<User>();
            Books = Set<Book>();
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Book>? Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.BooksTaken)
                .WithOne(ub=>ub.BooksTakenBy)
                .HasForeignKey(ub=>ub.UserEmail)
                .OnDelete(DeleteBehavior.Cascade);
                
            base.OnModelCreating(modelBuilder);
        }
    }
}
