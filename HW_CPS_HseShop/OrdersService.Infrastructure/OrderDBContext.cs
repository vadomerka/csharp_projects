using HseShopTransactions;
using Microsoft.EntityFrameworkCore;
using OrdersService.Entities.Common;

namespace OrdersService.Infrastructure
{
    public class OrderDBContext : DbContext
    {
        public OrderDBContext(DbContextOptions<OrderDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

            //modelBuilder.Entity<OrderStatus>()
            //.Property(u => u.Id)
            //.ValueGeneratedOnAdd();

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
            });
        }

        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
    }
}
