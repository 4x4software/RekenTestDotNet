using RekenTest.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RekenTest.Common.Implementers
{
    public class ProblemCalculator
    {
        public static bool MakeDecimalesEqual(ref IProblemValue valueA, ref IProblemValue valueB)
        {
            if (valueA.Decimals == valueB.Decimals)
                return true;

            // assign to valueToKeep the value with the highest number of decimals
            // valueToChange needs to increase the number of decimals to make them equal
            IProblemValue valueToChange = valueA; // valueA.Decimals < valueB.Decimals
            IProblemValue valueToKeep = valueB; // valueA.Decimals < valueB.Decimals

            if (valueA.Decimals > valueB.Decimals)
            {
                valueToChange = valueB;
                valueToKeep = valueA;
            };

            uint newValue = valueToChange.Value * (uint)Math.Pow(10, valueToKeep.Decimals - valueToChange.Decimals);
            if (newValue > ProblemValueTypes.MaxProblemValue)
                // invalid, exit before making changes to the ref parameter values
                return false;

            valueToChange.Value = newValue;
            valueToChange.Decimals = valueToKeep.Decimals;
            return true;
        }

        public static bool AddProblemValues(IProblemValue valueA, IProblemValue valueB, IProblemValue answer)
        {
            IProblemValue tempValueA = new ProblemValue();
            tempValueA.Assign(valueA);
            IProblemValue tempValueB = new ProblemValue();
            tempValueB.Assign(valueB);

            if (!MakeDecimalesEqual(ref tempValueA, ref tempValueB))
                return false;

            if ((tempValueA.Value + tempValueB.Value) > ProblemValueTypes.MaxProblemValue)
                return false;

            answer.Value = tempValueA.Value + tempValueB.Value;
            answer.Decimals = tempValueA.Decimals;

            return true;
        }
    }
}
