using NUnit.Framework;
using RekenTest.Common.Implementers;
using RekenTest.Common.Interfaces;
using System;

namespace RekenTest.Common.Tests
{
    public class ProblemValueTests : ProblemTestBase
    {
        private IProblemValue classSUT = null;

        [SetUp]
        public void Setup()
        {
            classSUT = problemValueFactory.NewProblemValue();
        }

        [Test]
        public void Assign_Test()
        {
            IProblemValue sourceValue = problemValueFactory.NewProblemValue();
            sourceValue.Value = 123;
            sourceValue.Decimals = 2;

            classSUT.Assign(sourceValue);
            Assert.AreEqual(sourceValue.Value, classSUT.Value);
            Assert.AreEqual(sourceValue.Decimals, classSUT.Decimals);
        }

        [Test]
        public void Clear_Test()
        {
            IProblemValue classSUT = problemValueFactory.NewProblemValue();
            classSUT.Value = 123;
            classSUT.Decimals = 2;
         
            classSUT.Clear();

            Assert.AreEqual(0, classSUT.Value);
            Assert.AreEqual(0,classSUT.Decimals);
        }

        [Test]
        public void SetPropertyValues_ShouldAssignValues()
        {
            classSUT.Value = 123;
            classSUT.Decimals = 2;

            Assert.AreEqual(123, classSUT.Value);
            Assert.AreEqual(2, classSUT.Decimals);
        }

        [Test]
        public void IsValid_Tests()
        {
            classSUT.Value = 0;
            Assert.IsTrue(classSUT.IsValid());
            classSUT.Value = ProblemValueTypes.MaxProblemValue;
            Assert.IsTrue(classSUT.IsValid());
            classSUT.Value = ProblemValueTypes.MaxProblemValue + 1;
            Assert.IsFalse(classSUT.IsValid());

            classSUT.Value = 0;
            classSUT.Decimals = 0;
            Assert.IsTrue(classSUT.IsValid());
            classSUT.Decimals = ProblemValueTypes.MaxDecimalDigits;
            Assert.IsTrue(classSUT.IsValid());
            classSUT.Decimals = ProblemValueTypes.MaxDecimalDigits + 1;
            Assert.IsFalse(classSUT.IsValid());
        }

        [Test]
        [TestCase("", "Empty value")]
        [TestCase("a", "")]
        [TestCase("9999999", "Value too large")]
        [TestCase("0.0000001", "Too many decimals")]
        public void ParseFromString_InvalidInputString_ShouldReturnFalse(string value, string message)
        {
            Assert.IsFalse(classSUT.ParseFromString(value), message);
        }

        [Test]
        [TestCase("0", "")]
        [TestCase("1", "")]
        [TestCase("1234567", "")]
        [TestCase("9999998", "Max value allowed")]
        public void ParseFromString_ValidIntValues_ShouldReturnTrue(string value, string message)
        {
            Assert.IsTrue(classSUT.ParseFromString(value), message);
        }

        [Test]
        [TestCase("0.1", "")]
        [TestCase("0.000001", "")]
        [TestCase("1.234", "")]
        [TestCase("123456.7", "")]
        [TestCase("1.234567", "")]
        public void ParseFromString_ValidDecimalValues_ShouldReturnTrue(string value, string message)
        {
            Assert.IsTrue(classSUT.ParseFromString(value), message);
        }

        [Test]
        [TestCase("0", 0, 0)]
        [TestCase("1", 1, 0)]
        [TestCase("0.1", 1, 1)]
        [TestCase("0.000001", 1, 6)]
        [TestCase("1.234", 1234, 3)]
        [TestCase("123456.7", 1234567, 1)]
        [TestCase("9999998", ProblemValueTypes.MaxProblemValue, 0)]
        public void ParseFromString_ValueTests(string input, int expectedValue, byte expectedDecimals)
        {
            Assert.IsTrue(classSUT.ParseFromString(input));

            Assert.AreEqual(expectedValue, classSUT.Value, input);
            Assert.AreEqual(expectedDecimals, classSUT.Decimals, input);
            Assert.IsTrue(classSUT.IsValid());
        }

        [Test]
        [TestCase("1", "1", "2")]
        [TestCase("1000", "0.001", "1000.001")]
        public void SetAnswerForValues_ValidAdd_Tests(string inputValueA, string inputValueB, string inputExpected)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);
            IProblemValue expectedValue = problemValueFactory.NewProblemValue(inputExpected);

            Assert.IsTrue(classSUT.SetAnswerForValues(ProblemType.ptAdd, valueA, valueB));

            Assert.AreEqual(expectedValue.Value, classSUT.Value);
            Assert.AreEqual(expectedValue.Decimals, classSUT.Decimals);
        }

        [Test]
        [TestCase("9999998", "1")]
        [TestCase("1000000", "0.000001")]
        public void SetAnswerForValues_InvalidAdd_Tests(string inputValueA, string inputValueB)
        {
            IProblemValue valueA = problemValueFactory.NewProblemValue(inputValueA);
            IProblemValue valueB = problemValueFactory.NewProblemValue(inputValueB);

            Assert.IsFalse(classSUT.SetAnswerForValues(ProblemType.ptAdd, valueA, valueB));
        }

        [Test]
        [TestCase("1", "1")]
        [TestCase("1.0", "1")]
        [TestCase("1.0000000", "1")]
        [TestCase("0.1", "0.1")]
        [TestCase("0.10", "0.1")]
        [TestCase("0.1000000", "0.1")]
        [TestCase("0.000010", "0.00001")]
        [TestCase("1000.010", "1000.01")]
        public void RemoveTrailingZeros_Tests(string inputValue, string expectedValue)
        {
            IProblemValue value = problemValueFactory.NewProblemValue(inputValue);
            IProblemValue expected = problemValueFactory.NewProblemValue(expectedValue);

            value.RemoveTrailingZeros();

            Assert.AreEqual(expected.Value, value.Value);
            Assert.AreEqual(expected.Decimals, value.Decimals);
        }

        [TestCase("0", "0")]
        [TestCase("1", "1")]
        [TestCase("0.1", "0.1")]
        [TestCase("0.000001", "0.000001")]
        [TestCase("1.234", "1.234")]
        [TestCase("123456.7", "123456.7")]
        [TestCase("9999998", "9999998")]
        public void IsEqualTo_True_Tests(string someValue, string otherValue)
        {
            IProblemValue some = problemValueFactory.NewProblemValue(someValue);
            IProblemValue other = problemValueFactory.NewProblemValue(otherValue);

            Assert.IsTrue(some.IsEqualTo(other));
        }

        [TestCase("0", "1")]
        [TestCase("1", "2")]
        [TestCase("0.1", "0.2")]
        [TestCase("0.000001", "0.00001")]
        [TestCase("1.234", "1.2345")]
        [TestCase("123456.7", "1.234567")]
        [TestCase("9999998", "999998")]
        public void IsEqualTo_False_Tests(string someValue, string otherValue)
        {
            IProblemValue some = problemValueFactory.NewProblemValue(someValue);
            IProblemValue other = problemValueFactory.NewProblemValue(otherValue);

            Assert.IsFalse(some.IsEqualTo(other));
        }
    }
}