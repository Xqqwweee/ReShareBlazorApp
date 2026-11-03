using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; 

namespace ReShareBlazorApp.Data
{
    public class ReShareDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ReShareDbContext(DbContextOptions<ReShareDbContext> options) 
            :base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
