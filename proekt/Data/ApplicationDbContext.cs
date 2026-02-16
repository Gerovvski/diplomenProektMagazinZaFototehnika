using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace proekt.Data
{
    public class ApplicationDbContext : IdentityDbContext<Client>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Cart> Carts { get; set; }
      //  public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
     //   public DbSet<Inventory> Inventories { get; set; }
     //   public DbSet<Order> Orders { get; set; }
      //  public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }

      

    }
}
