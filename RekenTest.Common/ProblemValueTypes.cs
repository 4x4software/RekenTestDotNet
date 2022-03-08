using System;
using System.Collections.Generic;
using System.Text;

namespace RekenTest.Common
{
    public enum ProblemType { ptAdd, ptSubtract, ptMultiply, ptDivide };

    public static class ProblemValueTypes
    {
        public const int MaxProblemValue = 9999998;
        public const byte MaxDecimalDigits = 6;
    }
}
