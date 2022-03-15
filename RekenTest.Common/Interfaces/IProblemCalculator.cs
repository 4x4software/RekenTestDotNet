using System;
using System.Collections.Generic;
using System.Text;

namespace RekenTest.Common.Interfaces
{
    public interface IProblemCalculator
    {
        public void MakeDecimalesEqual(ref IProblemValue valueA, ref IProblemValue valueB);
        public IProblemValue AddProblemValues(IProblemValue valueA, IProblemValue valueB);
    }
}
