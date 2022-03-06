using NUnit.Framework;
using RekenTest.Common.Implementers;
using System;

namespace RekenTest.Common.Tests
{
    public class ProblemValueTests
    {
        private ProblemValue ClassSUT = null;

        [SetUp]
        public void Setup()
        {
            ClassSUT = new ProblemValue();
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
        public void SetPropertyValues_ShouldFailOnValuesTooHigh()
        {
            Assert.Throws<Exception>(() =>
            {
                ClassSUT.Value = ProblemValueTypes.MaxProblemValue + 1;
            });

            Assert.Throws<Exception>(() =>
            {
                ClassSUT.Decimals = ProblemValueTypes.MaxDecimalDigits + 1;
            });
        }

        [Test]
        [TestCase("", "Empty value")]
        [TestCase("a", "")]
        [TestCase("9999999", "Value too large")]
        [TestCase("0.0000001", "Too many decimals")]
        public void ParseFromStringParseFromString_InvalidInputString_ShouldReturnFalse(string value, string message)
        {
            Assert.IsFalse(ClassSUT.ParseFromString(value), message);
        }

        [Test]
        [TestCase("0", "")]
        [TestCase("1", "")]
        [TestCase("1234567", "")]
        [TestCase("9999998", "Max value allowed")]
        public void ParseFromStringParseFromString_ValidIntValues_ShouldReturnTrue(string value, string message)
        {
            Assert.IsTrue(ClassSUT.ParseFromString(value), message);
        }

        [Test]
        [TestCase("0.1", "")]
        [TestCase("0.000001", "")]
        [TestCase("1.234", "")]
        [TestCase("123456.7", "")]
        public void ParseFromStringParseFromString_ValidDecimalValues_ShouldReturnTrue(string value, string message)
        {
            Assert.IsTrue(ClassSUT.ParseFromString(value), message);
        }
    }
}