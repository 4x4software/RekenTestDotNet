using NUnit.Framework;
using RekenTest.Common.Implementers;
using RekenTest.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RekenTest.Common.Tests
{
    [TestFixture]
    public class ProblemCalculatorTests : ProblemValueTestBase
    {
        private IProblemCalculator ClassSUT = null;

        [SetUp]
        public void Setup()
        {
            ClassSUT = new ProblemCalculator();
        }

        [Test]
        [TestCase("1000000", "1000000", 0)]
        [TestCase("1.2345", "1.2345", 4)]
        [TestCase("1.234", "1.2345", 4)]
        [TestCase("1.2345", "1.234", 4)]
        [TestCase("0.123456", "1.2", 6)]
        [TestCase("1.2", "1.234567", 6)]
        public void MakeDecimalsEqualTests(string inputValueA, string inputValueB, byte expectedDecimals)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue();
            valueA.ParseFromString(inputValueA);

            IProblemValue valueB = problemValueFactory.NewProblemValue();
            valueB.ParseFromString(inputValueB);

            ClassSUT.MakeDecimalesEqual(ref valueA, ref valueB);

            Assert.AreEqual(expectedDecimals, valueA.Decimals);
            Assert.AreEqual(valueA.Decimals, valueB.Decimals);
        }

        [TestCase("1", "1", "2")]
        [Test]
        public void AddTests(string inputValueA, string inputValueB, string expectedValue)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue();
            valueA.ParseFromString(inputValueA);

            IProblemValue valueB = problemValueFactory.NewProblemValue();
            valueB.ParseFromString(inputValueB);
        }
    }
}
