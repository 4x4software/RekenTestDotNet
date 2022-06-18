using System;
using System.Collections.Generic;
using System.Text;

namespace RekenTest.Common.Interfaces
{
    public interface IProblem
    {
        public void AssignValues(ProblemType problemType, IProblemValue problemValueA, IProblemValue problemValueB);
        public bool ParseFromString(string problemAsText);
        public IProblemValue GetCorrectAnswer();
        
        public IProblemValue ValueA { get; }
        public IProblemValue ValueB { get; }
        public ProblemType Type { get; }
    }
}
