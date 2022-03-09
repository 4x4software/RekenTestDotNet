﻿using RekenTest.Common;
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

        public bool ParseFromString(string inputValue)
        {
            try
            {
                // find decimal separator index
                int decimalSeparatorIndex = inputValue.IndexOf(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                // strip decimal separator from input string
                string valueAsString = (decimalSeparatorIndex < 0) ? inputValue : inputValue.Remove(decimalSeparatorIndex, 1);

                // Example: inputValue="1.234" results in: _value=1234 _decimals=3
                _value = Convert.ToUInt32(valueAsString);
                _decimals = (decimalSeparatorIndex <= 0) ? (byte)0 : (byte)(inputValue.Length - decimalSeparatorIndex - 1);

                return IsValid();
            }
            catch
            {
                return false;
            }
        }
    }
}
