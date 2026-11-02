using Microsoft.EntityFrameworkCore; 

namespace ReShareBlazorApp.Data
{
    public class ReShareDbContext : DbContext
    {
        public ReShareDbContext(DbContextOptions<ReShareDbContext> options) 
            :base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
