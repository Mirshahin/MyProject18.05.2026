using Microsoft.EntityFrameworkCore;
using MyProject18._05._2026.Models;
using Range = MyProject18._05._2026.Models.Range;

namespace MyProject18._05._2026.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Range> Ranges { get; set; }
    }
}
