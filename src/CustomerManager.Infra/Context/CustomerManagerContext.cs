using CustomerManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerManager.Infra.Context
{
    public class CustomerManagerContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Favorites> Favorites { get; set; }

        public CustomerManagerContext(DbContextOptions<CustomerManagerContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder){}
    }
}
