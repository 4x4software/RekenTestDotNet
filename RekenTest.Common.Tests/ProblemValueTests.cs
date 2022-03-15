using NUnit.Framework;
using RekenTest.Common.Implementers;
using RekenTest.Common.Interfaces;
using System;

namespace RekenTest.Common.Tests
{
    public class ProblemValueTests
    {
        private IProblemValue ClassSUT = null;

        [SetUp]
        public void Setup()
        {
            ClassSUT = new ProblemValue();
        }

        [Test]
        public void Assign_Test()
        {
            IProblemValue sourceValue = new ProblemValue();
            sourceValue.Value = 123;
            sourceValue.Decimals = 2;

            ClassSUT.Assign(sourceValue);
            Assert.AreEqual(sourceValue.Value, ClassSUT.Value);
            Assert.AreEqual(sourceValue.Decimals, ClassSUT.Decimals);
        }

        [Test]
        public void SetPropertyValues_ShouldAssignValues()
        {
            ClassSUT.Value = 123;
            ClassSUT.Decimals = 2;

            Assert.AreEqual(123, ClassSUT.Value);
            Assert.AreEqual(2, ClassSUT.Decimals);
        }

        [Test]
        public void IsValid_Tests()
        {
            ClassSUT.Value = 0;
            Assert.IsTrue(ClassSUT.IsValid());
            ClassSUT.Value = ProblemValueTypes.MaxProblemValue;
            Assert.IsTrue(ClassSUT.IsValid());
            ClassSUT.Value = ProblemValueTypes.MaxProblemValue + 1;
            Assert.IsFalse(ClassSUT.IsValid());

            ClassSUT.Value = 0;
            ClassSUT.Decimals = 0;
            Assert.IsTrue(ClassSUT.IsValid());
            ClassSUT.Decimals = ProblemValueTypes.MaxDecimalDigits;
            Assert.IsTrue(ClassSUT.IsValid());
            ClassSUT.Decimals = ProblemValueTypes.MaxDecimalDigits + 1;
            Assert.IsFalse(ClassSUT.IsValid());
        }

        [Test]
        [TestCase("", "Empty value")]
        [TestCase("a", "")]
        [TestCase("9999999", "Value too large")]
        [TestCase("0.0000001", "Too many decimals")]
        public void ParseFromString_InvalidInputString_ShouldReturnFalse(string value, string message)
        {
            Assert.IsFalse(ClassSUT.ParseFromString(value), message);
        }

        [Test]
        [TestCase("0", "")]
        [TestCase("1", "")]
        [TestCase("1234567", "")]
        [TestCase("9999998", "Max value allowed")]
        public void ParseFromString_ValidIntValues_ShouldReturnTrue(string value, string message)
        {
            Assert.IsTrue(ClassSUT.ParseFromString(value), message);
        }

        [Test]
        [TestCase("0.1", "")]
        [TestCase("0.000001", "")]
        [TestCase("1.234", "")]
        [TestCase("123456.7", "")]
        [TestCase("1.234567", "")]
        public void ParseFromString_ValidDecimalValues_ShouldReturnTrue(string value, string message)
        {
            Assert.IsTrue(ClassSUT.ParseFromString(value), message);
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
            Assert.IsTrue(ClassSUT.ParseFromString(input));

            Assert.AreEqual(expectedValue, ClassSUT.Value, input);
            Assert.AreEqual(expectedDecimals, ClassSUT.Decimals, input);
            Assert.IsTrue(ClassSUT.IsValid());
        }
    }
}