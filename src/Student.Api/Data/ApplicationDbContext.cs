using Microsoft.EntityFrameworkCore;
using StudentApi.Models;
using System.Collections.Generic;

namespace StudentApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin123",
                    Role = "Admin"
                },
                new User
                {
                    Id = 2,
                    Username = "user",
                    Password = "user123",
                    Role = "User"
                }
            );
        }


    }
}
