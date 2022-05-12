using RekenTest.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RekenTest.Common.Implementers
{
    public class Problem : IProblem
    {
        private IProblemValue _problemValueA;
        private IProblemValue _problemValueB;
        private ProblemType _problemType;

        public IProblemValue ValueA { get { return _problemValueA; } }
        public IProblemValue ValueB { get { return _problemValueB; } }
        public ProblemType Type { get { return _problemType; } }

        public void AssignValues(ProblemType problemType, IProblemValue problemValueA, IProblemValue problemValueB)
        {
            _problemType = problemType;
            _problemValueA = problemValueA;
            _problemValueB = problemValueB;
        }

        public IProblemValue GetCorrectAnswer()
        {
            throw new NotImplementedException();
        }

        public bool ParseFromString(string problemAsText)
        {
            return false;
        }
    }
}
