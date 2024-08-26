using Microsoft.EntityFrameworkCore;

namespace StoreAPIWebApp.Models
{
    public class StoreAPIContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<User> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        public StoreAPIContext(DbContextOptions<StoreAPIContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
