using FullStack.Models;
using FullStack.Repository;
using Microsoft.EntityFrameworkCore;

namespace FullStack.Data
{
    public class FullStackDbContext : DbContext
    {
        public FullStackDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> EmpTabs { get; set; }
        public DbSet<Models.Review> AddReview { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
