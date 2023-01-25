using RekenTest.Common.Interfaces;

namespace RekenTest.Database.Interfaces
{
    public interface IProblemEntity
    {
        public int Id { get; set; }
        public void Assign(IProblem inputValue);
    }
}
