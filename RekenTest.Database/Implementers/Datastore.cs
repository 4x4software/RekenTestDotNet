using Microsoft.EntityFrameworkCore.Storage;
using RekenTest.Common.Implementers;
using RekenTest.Common.Interfaces;
using RekenTest.Database.DbContexts;
using RekenTest.Database.DTO;
using RekenTest.Database.Interfaces;
namespace RekenTest.Database.Implementers
{
    public class Datastore : IDatastore
    {
        public bool StoreProblem(IProblem problem)
        {
            var db = new ProblemDataDbContext();
            var problemEntity = new ProblemEntity();
            problemEntity.Assign(problem);

            if (db.Problems == null)
                return false;

            db.Problems.Add(problemEntity);
            return db.SaveChanges() != 0; // number of saved entries
        }
    }
}
