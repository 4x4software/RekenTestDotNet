using System;
using System.Collections.Generic;
using System.Text;

namespace RekenTest.Common.Interfaces
{
    public interface IProblem
    {
        public void AssignValues(ProblemType problemType, IProblemValue problemValueA, IProblemValue problemValueB);
        public IProblemValue GetCorrectAnswer();
        public bool IsValid();
        public bool ParseFromString(string problemAsText);
        public ProblemType Type { get; }
        public IProblemValue ValueA { get; }
        public IProblemValue ValueB { get; }
    }
}
