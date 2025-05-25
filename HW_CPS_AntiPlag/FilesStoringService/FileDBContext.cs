using FilesStoringService.Models;
using Microsoft.EntityFrameworkCore;

namespace FilesStoringService
{
    public class FileDBContext : DbContext
    {
        public FileDBContext(DbContextOptions<FileDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFile>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        }

        public DbSet<UserFile> UserFiles { get; set; } = null!;
    }
}
