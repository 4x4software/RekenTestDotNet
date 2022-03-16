using NUnit.Framework;
using RekenTest.Common.Implementers;
using RekenTest.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RekenTest.Common.Tests
{
    public class ProblemValueTestBase
    {
        protected IProblemValueFactory problemValueFactory = null;

        [OneTimeSetUp]
        public void SetupFixture()
        {
            problemValueFactory = new ProblemValueFactory();
        }
    }
}
