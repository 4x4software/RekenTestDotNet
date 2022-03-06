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

        public bool ParseFromString(string problemValueAsText)
        {
            try
            {
                // find decimal separator position
                var decimalSeparatorPosition = problemValueAsText.IndexOf(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                if (decimalSeparatorPosition < 0)
                {
                    // no decimal separator found
                    _value = Convert.ToUInt32(problemValueAsText);
                }
                else
                {
                    _value = Convert.ToUInt32(problemValueAsText.Substring(0, decimalSeparatorPosition));
                    var temp = problemValueAsText.Substring(decimalSeparatorPosition + 1, problemValueAsText.Length - decimalSeparatorPosition - 1);
                    _decimals = Convert.ToByte(temp);
                    return true;
                }

                return (_value <= ProblemValueTypes.MaxProblemValue);
            }
            catch
            {
                return false;
            }
        }
    }
}
