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

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .IsRequired();

                entity.Property(e => e.NotificationKey)
                    .IsRequired();

                entity.Property(e => e.Payload)
                    .IsRequired();

                entity.Property(e => e.IsProcessed)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.HasIndex(e => e.NotificationKey)
                    .IsUnique();
            });
        }

        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
    }
}
