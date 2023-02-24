using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Market.Models
{
  public class MarketContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserPurchase> UserPurchases { get; set; }

    public MarketContext(DbContextOptions options) : base(options) { }
  }
}