using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;
namespace ExpenseTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 

        }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<Expense>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId);


        }
    }
}
