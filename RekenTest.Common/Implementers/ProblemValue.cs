using RekenTest.Common;
using RekenTest.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace RekenTest.Common.Implementers
{
    public class ProblemValue : IProblemValue
    {
        public uint Value { get; set; } = 0;

        public byte Decimals { get; set; } = 0;

        public void Assign(IProblemValue source)
        {
            Value = source.Value;
            Decimals = source.Decimals;
        }

        public bool IsValid()
        {
            return (Value >= 0) && (Value <= ProblemValueTypes.MaxProblemValue) && (Decimals >= 0) && (Decimals <= ProblemValueTypes.MaxDecimalDigits);
        }

        public bool ParseFromString(string inputValue)
        {
            try
            {
                // find decimal separator index
                int decimalSeparatorIndex = inputValue.IndexOf(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                // strip decimal separator from input string
                string valueAsString = (decimalSeparatorIndex < 0) ? inputValue : inputValue.Remove(decimalSeparatorIndex, 1);

                // Example: inputValue="1.234" results in: _value=1234 _decimals=3
                Value = Convert.ToUInt32(valueAsString);
                Decimals = (decimalSeparatorIndex <= 0) ? (byte)0 : (byte)(inputValue.Length - decimalSeparatorIndex - 1);

                return IsValid();
            }
            catch
            {
                return false;
            }
        }

        public void RemoveTrailingZeros()
        {
            while (Decimals > 0) 
            {
                if (Value % 10 == 0)
                {
                    Value = Value / 10; // note that / operator is an integer division
                    Decimals--;
                }
                else
                    break;
            }
        }

        public bool SetAnswerForValues(ProblemType problemType, IProblemValue valueA, IProblemValue valueB)
        {
            switch (problemType)
            {
                case ProblemType.ptAdd:
                    {
                        return ProblemCalculator.AddProblemValues(valueA, valueB, this);
                    }
                default:
                    {
                        throw new NotImplementedException("SetAnswerForValues");
                    }
            }
        }
    }

    public class ProblemValueFactory : IProblemValueFactory
    {
        public IProblemValue NewProblemValue()
        {
            return new ProblemValue();
        }

        public IProblemValue NewProblemValue(string value)
        {
            var problemValue = NewProblemValue();

            problemValue.ParseFromString(value);

            return problemValue;
        }
    }
}
