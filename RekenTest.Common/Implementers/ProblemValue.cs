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
            set { _value = value; }
        }
        public byte Decimals
        {
            get { return _decimals; }
            set { _decimals = value; }
        }

        public bool IsValid()
        {
            return (_value >= 0) && (_value <= ProblemValueTypes.MaxProblemValue) && (_decimals >= 0) && (_decimals <= ProblemValueTypes.MaxDecimalDigits);
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
                    _decimals = 0;
                }
                else
                {
                    string valuePart = problemValueAsText.Substring(0, decimalSeparatorPosition);
                    string decimalPart = problemValueAsText.Substring(decimalSeparatorPosition + 1, problemValueAsText.Length - decimalSeparatorPosition - 1);
                    // value = string without decimal separator
                    _value = Convert.ToUInt32(valuePart + decimalPart);
                    _decimals = Convert.ToByte(decimalPart.Length);
                }

                return IsValid();
            }
            catch
            {
                return false;
            }
        }
    }
}
