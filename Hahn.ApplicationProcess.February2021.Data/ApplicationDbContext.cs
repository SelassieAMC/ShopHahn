using Hahn.ApplicationProcess.February2021.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.February2021.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasKey(a => a.ID);
                entity.HasOne(a => a.Deparment)
                .WithMany(s => s.Assets);
            });
        }
    }
}
