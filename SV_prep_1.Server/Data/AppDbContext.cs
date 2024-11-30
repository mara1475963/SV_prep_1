using Microsoft.EntityFrameworkCore;
using SV_prep_1.Server.Models;

namespace SV_prep_1.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
