using Microsoft.EntityFrameworkCore;
using chatapi.Model;
namespace chatapi.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> context) : base(context) { }
        public DbSet <models> Users { get; set; }
        public DbSet<loggedin> loggedin { get; set; }
        public DbSet<Login> login { get; set; } 
        public DbSet<gPrice> Plans { get; set; }

        public DbSet<paymentt> Payment { get; set; }
    }
}
