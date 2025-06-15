using HseShopTransactions;
using Microsoft.EntityFrameworkCore;
using HseShopTransactions.Entities.Common;

namespace HseShopTransactions.Infrastructure
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

                entity.Property(e => e.Payload)
                    .IsRequired();

                entity.Property(e => e.IsProcessed)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                //entity.HasIndex(e => e.NotificationKey)
                    //.IsRequired();
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .IsRequired();

                entity.Property(e => e.Payload)
                    .IsRequired();

                entity.Property(e => e.IsSent)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();
            });
        }

        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
    }
}
