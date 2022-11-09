using RekenTest.Common;
using RekenTest.Common.Interfaces;
using RekenTest.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RekenTest.Database.DTO
{
    public class ProblemEntity : IProblemEntity
    {
        public int Id { get; set; } = 0;

        public string ProblemValueA { get; set; } = "";
        public string ProblemValueB { get; set; } = "";

        public ProblemType ProblemType { get; set; }

        public void Assign(IProblem inputValue)
        {
            ProblemValueA = inputValue.ValueA.ToString() ?? "";
            ProblemValueB = inputValue.ValueB.ToString() ?? "";
            ProblemType = inputValue.Type;
        }
    }
}
