using System;
using System.Collections.Generic;
using System.Text;

namespace RekenTest.Common
{
    public enum ProblemType { ptAdd, ptSubtract, ptMultiply, ptDivide };

    public static class ProblemValueTypes
    {
        public const uint MaxProblemValue = 9999998;
        public const byte MaxDecimalDigits = 6;
    }
}
