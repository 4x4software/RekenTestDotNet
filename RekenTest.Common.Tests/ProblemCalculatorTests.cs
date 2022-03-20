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
        [Test]
        [TestCase("1000000", "1000000", 0)]
        [TestCase("1.2345", "1.2345", 4)]
        [TestCase("1.234", "1.2345", 4)]
        [TestCase("1.2345", "1.234", 4)]
        [TestCase("0.123456", "1.2", 6)]
        [TestCase("1.2", "1.234567", 6)]
        public void MakeDecimalsEqual_ValidValueCombinations(string inputValueA, string inputValueB, byte expectedDecimals)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);

            Assert.IsTrue(ProblemCalculator.MakeDecimalesEqual(ref valueA, ref valueB));

            Assert.AreEqual(expectedDecimals, valueA.Decimals);
            Assert.AreEqual(valueA.Decimals, valueB.Decimals);
        }

        [Test]
        [TestCase("1000000", "0.000001")]
        [TestCase("0.000001", "1000000")]
        [TestCase("0.1", "1000000")]
        [TestCase("1000000", "0.1")]
        [TestCase("10", "0.000001")]
        [TestCase("0.000001", "10")]
        public void MakeDecimalsEqual_InvalidValueCombinations(string inputValueA, string inputValueB)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);

            Assert.IsFalse(ProblemCalculator.MakeDecimalesEqual(ref valueA, ref valueB));
        }

        [TestCase("0.1", "1000000")]
        [TestCase("1000000", "0.1")]
        public void MakeDecimalsEqual_InvalidValueCombinations_ShouldNotChangeInputValues(string inputValueA, string inputValueB)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);

            IProblemValue originalValueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue originalValueB = problemValueFactory.NewProblemValue(inputValueB);

            Assert.IsFalse(ProblemCalculator.MakeDecimalesEqual(ref valueA, ref valueB));

            Assert.AreEqual(originalValueA.Decimals, valueA.Decimals);
            Assert.AreEqual(originalValueA.Value, valueA.Value);

            Assert.AreEqual(originalValueB.Decimals, valueB.Decimals);
            Assert.AreEqual(originalValueB.Value, valueB.Value);
        }

        [TestCase("1", "1", "2")]
        [TestCase("9999997", "1", "9999998")]
        [TestCase("9000", "0.001", "9000.001")]
        [TestCase("1", "0.000001", "1.000001")]
        [Test]
        public void AddProblemValues_Valid_Tests(string inputValueA, string inputValueB, string expectedValue)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);
            IProblemValue expected = problemValueFactory.NewProblemValue(expectedValue);
            IProblemValue actual = problemValueFactory.NewProblemValue();

            Assert.IsTrue(ProblemCalculator.AddProblemValues(valueA, valueB, actual));

            Assert.AreEqual(expected.Decimals, actual.Decimals);
            Assert.AreEqual(expected.Value, actual.Value);
        }

        [TestCase("9999998", "1", "Value too high")]
        [TestCase("9000", "0.0001", "Value+decimals too high")]
        [TestCase("1", "0.0000001", "Too many decimals")]
        [Test]
        public void AddProblemValues_Invalid_Tests(string inputValueA, string inputValueB, string message)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);
            IProblemValue actual = problemValueFactory.NewProblemValue();

            Assert.IsFalse(ProblemCalculator.AddProblemValues(valueA, valueB, actual), message);
        }
    }
}
