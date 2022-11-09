using RekenTest.Database.DTO;
using Microsoft.EntityFrameworkCore;

namespace RekenTest.Database.DbContexts
{
    public class ProblemDataDbContext : DbContext
    {
        public DbSet<ProblemEntity>? Problems { get; set; }

        public ProblemDataDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(
          DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=c:\\temp\\test.db");
        }
    }
}