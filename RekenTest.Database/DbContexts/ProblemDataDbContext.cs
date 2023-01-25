using System;
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
            string dataBaseFileName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\Rekentest.db";
            if (System.IO.File.Exists(dataBaseFileName))
                // TODO: Use ILogger here
                Console.WriteLine($"Using database file {dataBaseFileName}");
            else
                Console.WriteLine($"Creating new database file {dataBaseFileName}");

            optionsBuilder.UseSqlite("Data Source=" + dataBaseFileName);
        }
    }
}