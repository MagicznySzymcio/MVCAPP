using Microsoft.EntityFrameworkCore;
using MVCAPP.Models;

namespace MVCAPP.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Test> Tests { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = 1,
                Type = "Root"
            },
            new Role
            {
                Id = 2,
                Type = "Administrator"
            },
            new Role
            {
                Id = 3,
                Type = "User"
            });

            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "root",
                Password = "2eIDQOzq52bN8+neK99ozNzVGWdG3O9gfeYaCkhDIQk=",
                Salt = "dh5t7igbmnY3iBwKXlDIMQ==",
                RoleId = 1
            },
            new User
            {
                Id = 2,
                Username = "admin",
                Password = "HUftzfjHe1Zd04UA5fJum9qf4U6A/IoqRWZwC3rlGII=",
                Salt = "y8MUSe1OtzQkeTwSlOkTcQ==",
                RoleId = 2
            },
            new User
            {
                Id = 3,
                Username = "jan",
                Password = "fVjyV6H9ljRWRMunvObkqTLZcoyZaxewyrkVywR4pG8=",
                Salt = "GEDp/ff5TScRoPrW2qoT/g==",
                RoleId = 3
            });
        }
    }
}