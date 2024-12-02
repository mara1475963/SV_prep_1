using Microsoft.EntityFrameworkCore;
using SV_prep_1.Server.Models;

namespace SV_prep_1.Server.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Store)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.StoreId);

            modelBuilder.Entity<Store>()
               .HasOne(s => s.Address)
               .WithOne(a => a.Store)
               .HasForeignKey<Store>(s => s.AddressId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
