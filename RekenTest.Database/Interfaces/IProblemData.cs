using RekenTest.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RekenTest.Database.Interfaces
{
    public interface IProblemData
    {
        public int Id { get; set; }

        public void Assign(IProblem inputValue);
    }
}
