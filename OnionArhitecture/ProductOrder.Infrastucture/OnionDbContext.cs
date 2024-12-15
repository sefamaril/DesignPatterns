using Microsoft.EntityFrameworkCore;
using ProductOrder.Domain.Entities;

namespace ProductOrder.Infrastucture
{
    public class OnionDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public OnionDbContext(DbContextOptions<OnionDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}