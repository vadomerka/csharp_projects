using FileAnalisysService.Models;
using Microsoft.EntityFrameworkCore;

namespace FilesAnaliseService
{
    public class AnalisysDBContext : DbContext
    {
        public AnalisysDBContext(DbContextOptions<AnalisysDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileStatistics>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<FileCompare>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        }

        public DbSet<FileStatistics> FileStatistics { get; set; } = null!;
        public DbSet<FileCompare> FileCompare { get; set; } = null!;
    }
}
