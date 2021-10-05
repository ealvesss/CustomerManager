using CustomerManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerManager.Infra.Context
{
    public class CustomerManagerContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<Product> Product { get; set; }

        public CustomerManagerContext(DbContextOptions<CustomerManagerContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(f => f.Favorite)
                .WithMany(p => p.Products)
                .HasForeignKey(k => k.FavoriteId)
            .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
