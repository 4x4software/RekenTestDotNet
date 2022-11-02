using RekenTest.Common.Interfaces;
using RekenTest.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RekenTest.Database.DTO
{
    public class ProblemData : IProblemData
    {
        public int Id { get; set; } = 0;

        public void Assign(IProblem inputValue)
        {
            
        }
    }
}
