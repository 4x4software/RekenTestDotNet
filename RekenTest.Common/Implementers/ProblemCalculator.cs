using RekenTest.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RekenTest.Common.Implementers
{
    public static class ProblemCalculator
    {
        public static bool MakeDecimalsEqual(ref IProblemValue valueA, ref IProblemValue valueB)
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

        private static bool AddSubtractProblemValues(IProblemValue valueA, IProblemValue valueB, IProblemValue answer, bool subtractValues)
        {
            IProblemValue tempValueA = new ProblemValue();
            tempValueA.Assign(valueA);
            IProblemValue tempValueB = new ProblemValue();
            tempValueB.Assign(valueB);

            if (!MakeDecimalsEqual(ref tempValueA, ref tempValueB))
                return false;

            if (subtractValues)
            {
                if (tempValueA.Value < tempValueB.Value) // uint types, cant use minus symbol here 
                    return false;

                answer.Value = tempValueA.Value - tempValueB.Value;
            }
            else
            {
                if ((tempValueA.Value + tempValueB.Value) > ProblemValueTypes.MaxProblemValue)
                    return false;

                answer.Value = tempValueA.Value + tempValueB.Value;
            }

            answer.Decimals = tempValueA.Decimals;

            return true;
        }

        public static bool AddProblemValues(IProblemValue valueA, IProblemValue valueB, IProblemValue answer)
        {
            return AddSubtractProblemValues(valueA, valueB, answer, false);
        }

        public static bool SubtractProblemValues(IProblemValue valueA, IProblemValue valueB, IProblemValue answer)
        {
            return AddSubtractProblemValues(valueA, valueB, answer, true);
        }

        public static bool MultiplyProblemValues(IProblemValue valueA, IProblemValue valueB, IProblemValue answer)
        {
            IProblemValue tempValueA = new ProblemValue();
            tempValueA.Assign(valueA);
            IProblemValue tempValueB = new ProblemValue();
            tempValueB.Assign(valueB);

            answer.Value = tempValueA.Value * tempValueB.Value;
            answer.Decimals = (byte)(tempValueA.Decimals + tempValueB.Decimals);

            answer.RemoveTrailingZeros();

            return answer.IsValid();
        }

        public static bool DivideProblemValues(IProblemValue valueA, IProblemValue valueB, IProblemValue answer)
        {
            if (valueB.Value == 0)
                return false;

            int tempValueA = checked((int)valueA.Value);
            int tempValueB = checked((int)valueB.Value);

            // temp - no decimals answer
            int ignoreRemainder = 0;
            answer.Value = checked((uint)Math.DivRem(tempValueA, tempValueB, out ignoreRemainder));
            answer.Decimals = 0;

            int divideValue = tempValueA;
            byte extraDecimals = 0;

            while (((divideValue % valueB.Value) != 0) // % == modulus
                && (extraDecimals < ProblemValueTypes.MaxDecimalDigits))
            {
                divideValue = divideValue * 10;
                extraDecimals++;
            }

            int answerDecimals = valueA.Decimals - valueB.Decimals + extraDecimals;
            while (answerDecimals < 0 )
            {
                divideValue = divideValue * 10;
                answerDecimals++;
            }

            if ((divideValue % valueB.Value) != 0)
                // answer is not an integer, like 1/3
                return false;
            if (divideValue > ProblemValueTypes.MaxProblemValue)
                return false;
            if (answerDecimals > ProblemValueTypes.MaxDecimalDigits)
                return false;

            answer.Value = checked((uint)Math.DivRem(divideValue, checked((int)valueB.Value), out ignoreRemainder));
            answer.Decimals = checked((byte)answerDecimals);

            answer.RemoveTrailingZeros();

            return answer.IsValid();
        }
    }
}
