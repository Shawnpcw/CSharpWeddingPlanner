using Microsoft.EntityFrameworkCore;
 
namespace WeddingPlanner.Models
{
    public class weddingContext : DbContext
    {
        
        public weddingContext(DbContextOptions<weddingContext> options) : base(options) { }
        public DbSet<Guest> guests { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Wedding> weddings { get; set; }
        
    }
}
