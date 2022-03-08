using System;
using System.Collections.Generic;
using System.Text;

namespace RekenTest.Common.Interfaces
{
    public interface IProblemValue
    {
        uint Value { get; set; }
        byte Decimals { get; set; }
        public bool ParseFromString(string problemValueAsText);
        public bool IsValid();
    }
}
