using RekenTest.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RekenTest.Common.Implementers
{
    public class ProblemCalculator : IProblemCalculator
    {
        public void MakeDecimalesEqual(ref IProblemValue valueA, ref IProblemValue valueB)
        {
            if (valueA.Decimals == valueB.Decimals)
                return;

            if (valueA.Decimals > valueB.Decimals)
            {
                while (valueA.Decimals > valueB.Decimals)
                {
                    valueB.Value = valueB.Value * 10;
                    valueB.Decimals++;
                }
                return;
            }

            while (valueA.Decimals < valueB.Decimals)
            {
                valueA.Value = valueA.Value * 10;
                valueA.Decimals++;
            }
        }

        public IProblemValue AddProblemValues(IProblemValue valueA, IProblemValue valueB)
        {
            throw new NotImplementedException();
        }
    }
}
