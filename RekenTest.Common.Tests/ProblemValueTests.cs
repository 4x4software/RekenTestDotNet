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
    }
}