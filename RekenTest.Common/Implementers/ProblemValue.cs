using RekenTest.Common;
using RekenTest.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RekenTest.Common.Implementers
{
    public class ProblemValue : IProblemValue
    {
        private uint _value = 0;
        private byte _decimals = 0;

        public uint Value
        {
            get { return _value; }
            set
            {
                if (value > ProblemValueTypes.MaxProblemValue)
                    throw new Exception($"Value {value} is too high. Max value is {ProblemValueTypes.MaxProblemValue}");

                _value = value;
            }
        }
        public byte Decimals
        {
            get { return _decimals; }
            set
            {
                if (value > ProblemValueTypes.MaxDecimalDigits)
                    throw new Exception($"Value {value} too high. Max decimals is {ProblemValueTypes.MaxDecimalDigits}");

                _decimals = value;
            }
        }
    }
}
