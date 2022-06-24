using NUnit.Framework;
using RekenTest.Common.Implementers;
using RekenTest.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RekenTest.Common.Tests
{
    [TestFixture]
    public class ProblemCalculatorTests : ProblemTestBase
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

            Assert.IsTrue(ProblemCalculator.MakeDecimalsEqual(ref valueA, ref valueB));

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

            Assert.IsFalse(ProblemCalculator.MakeDecimalsEqual(ref valueA, ref valueB));
        }

        [TestCase("0.1", "1000000")]
        [TestCase("1000000", "0.1")]
        public void MakeDecimalsEqual_InvalidValueCombinations_ShouldNotChangeInputValues(string inputValueA, string inputValueB)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);

            IProblemValue originalValueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue originalValueB = problemValueFactory.NewProblemValue(inputValueB);

            Assert.IsFalse(ProblemCalculator.MakeDecimalsEqual(ref valueA, ref valueB));

            Assert.AreEqual(originalValueA.Decimals, valueA.Decimals);
            Assert.AreEqual(originalValueA.Value, valueA.Value);

            Assert.AreEqual(originalValueB.Decimals, valueB.Decimals);
            Assert.AreEqual(originalValueB.Value, valueB.Value);
        }

        [TestCase("1", "0", "1")]
        [TestCase("2", "1", "3")]
        [TestCase("9999997", "1", "9999998")]
        [TestCase("9000", "0.001", "9000.001")]
        [TestCase("1", "0.000001", "1.000001")]
        [Test]
        public void AddProblemValues_Valid_Tests(string inputValueA, string inputValueB, string expectedAddValue)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);
            IProblemValue expectedAdd = problemValueFactory.NewProblemValue(expectedAddValue);
            IProblemValue actual = problemValueFactory.NewProblemValue();

            Assert.IsTrue(ProblemCalculator.AddProblemValues(valueA, valueB, actual));

            Assert.AreEqual(expectedAdd.Decimals, actual.Decimals);
            Assert.AreEqual(expectedAdd.Value, actual.Value);
        }

        [TestCase("1", "1", "0")]
        [TestCase("2", "1", "1")]
        [TestCase("9999997", "1", "9999996")]
        [TestCase("9000", "0.001", "8999.999")]
        [TestCase("1", "0.000001", "0.999999")]
        [Test]
        public void SubtractProblemValues_Valid_Tests(string inputValueA, string inputValueB, string expectedSubtractValue)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);
            IProblemValue expectedSubtract = problemValueFactory.NewProblemValue(expectedSubtractValue);
            IProblemValue actual = problemValueFactory.NewProblemValue();

            Assert.IsTrue(ProblemCalculator.SubtractProblemValues(valueA, valueB, actual));

            Assert.AreEqual(expectedSubtract.Decimals, actual.Decimals);
            Assert.AreEqual(expectedSubtract.Value, actual.Value);
        }

        [TestCase("9999998", "1", "Answer too high")]
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

        [TestCase("1", "2", "Answer below zero")]
        [TestCase("9000", "0.0001", "Value+decimals too high")]
        [TestCase("1", "0.0000001", "Too many decimals")]
        [Test]
        public void SubtractProblemValues_Invalid_Tests(string inputValueA, string inputValueB, string message)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);
            IProblemValue actual = problemValueFactory.NewProblemValue();

            Assert.IsFalse(ProblemCalculator.SubtractProblemValues(valueA, valueB, actual), message);
        }

        [TestCase("1", "1", "1")]
        [TestCase("0.1", "0.2", "0.02")]
        [TestCase("2000", "2000", "4000000")]
        [TestCase("0.001", "2000", "2")]
        public void MultiplyProblemValues_Valid_Tests(string inputValueA, string inputValueB, string expectedMultiplyValue)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);
            IProblemValue expectedMultiply = problemValueFactory.NewProblemValue(expectedMultiplyValue);
            IProblemValue actual = problemValueFactory.NewProblemValue();

            Assert.IsTrue(ProblemCalculator.MultiplyProblemValues(valueA, valueB, actual));

            Assert.AreEqual(expectedMultiply.Decimals, actual.Decimals);
            Assert.AreEqual(expectedMultiply.Value, actual.Value);
        }

        [TestCase("10000", "1000", "Value too high")]
        [TestCase("0.0001", "0.0001", "Too many decimals")]
        public void MultiplyProblemValues_Invalid_Tests(string inputValueA, string inputValueB, string message)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);
            IProblemValue actual = problemValueFactory.NewProblemValue();

            Assert.IsFalse(ProblemCalculator.MultiplyProblemValues(valueA, valueB, actual), message);
        }

        [TestCase("6", "2", "3")]
        [TestCase("6", "20", "0.3")]
        [TestCase("0.6", "2", "0.3")]
        [TestCase("1.25", "0.25", "5")]
        [TestCase("1000", "0.001", "1000000")]
        [TestCase("0.001", "0.001", "1")]
        [TestCase("0.001", "1000", "0.000001")]
        public void DivideProblemValues_Valid_Tests(string inputValueA, string inputValueB, string expectedDivideValue)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);
            IProblemValue expectedDivide = problemValueFactory.NewProblemValue(expectedDivideValue);
            IProblemValue actual = problemValueFactory.NewProblemValue();

            Assert.IsTrue(ProblemCalculator.DivideProblemValues(valueA, valueB, actual));

            Assert.AreEqual(expectedDivide.Decimals, actual.Decimals);
            Assert.AreEqual(expectedDivide.Value, actual.Value);
        }

        [TestCase("10", "0", "Division by zero")]
        [TestCase("1", "3", "Answer not an integer")]
        [TestCase("1000", "0.0000001", "Answervalue too large")]
        [TestCase("0.0000001", "1000", "Answer has too many decimals")]
        public void DivideProblemValues_Invalid_Tests(string inputValueA, string inputValueB, string message)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);
            IProblemValue actual = problemValueFactory.NewProblemValue();

            Assert.IsFalse(ProblemCalculator.DivideProblemValues(valueA, valueB, actual), message);
        }

        [TestCase(ProblemType.ptAdd, "1", "2", "3")]
        [TestCase(ProblemType.ptSubtract, "4", "3", "1")]
        [TestCase(ProblemType.ptMultiply, "2", "3", "6")]
        [TestCase(ProblemType.ptDivide, "10", "5", "2")]
        public void CalculateCorrectAnswer_BasicTests_GoodInput(ProblemType problemType, string inputValueA, string inputValueB, string expectedAnswer)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);
            IProblemValue actualAnswer = problemValueFactory.NewProblemValue();
            
            Assert.IsTrue(ProblemCalculator.CalculateCorrectAnswer(problemType, valueA, valueB, actualAnswer));
            Assert.IsTrue(actualAnswer.IsEqualTo(problemValueFactory.NewProblemValue(expectedAnswer)));
        }
        
        [TestCase(ProblemType.ptAdd, "0.000001", "1000000")]
        [TestCase(ProblemType.ptSubtract, "4", "5")]
        [TestCase(ProblemType.ptMultiply, "0.000001", "0.000001")]
        [TestCase(ProblemType.ptDivide, "1", "3")]
        public void CalculateCorrectAnswer_BasicTests_BadInput(ProblemType problemType, string inputValueA, string inputValueB)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);
            IProblemValue actualAnswer = problemValueFactory.NewProblemValue();
            
            Assert.IsFalse(ProblemCalculator.CalculateCorrectAnswer(problemType, valueA, valueB, actualAnswer));
            // TODO: actual answer should be empty?
            // Assert.IsTrue(actualAnswer.IsEqualTo(problemValueFactory.NewProblemValue()));
        }
        
    }
}
