using Microsoft.EntityFrameworkCore;
using PaymentsService.Entities.Common;

namespace PaymentsService.Infrastructure
{
    public class AccountDBContext : DbContext
    {
        public AccountDBContext(DbContextOptions<AccountDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        }

        public DbSet<Account> Accounts { get; set; } = null!;
    }
}
