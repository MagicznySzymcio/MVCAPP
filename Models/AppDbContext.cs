using Microsoft.EntityFrameworkCore;
using MVCAPP.Models;

namespace MVCAPP.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
