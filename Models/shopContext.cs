using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Models
{
   public class shopContext : DbContext
   {
      public shopContext(DbContextOptions<shopContext> options) : base(options)
      {

      }

      public DbSet<Category> Categories { get; set; }
      public DbSet<Product> Products { get; set; }
   }
}
