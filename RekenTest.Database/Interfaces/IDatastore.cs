using RekenTest.Common.Interfaces;

namespace RekenTest.Database.Interfaces
{
    public interface IDatastore
    {
        public bool StoreProblem(IProblem problem);
    }
}
