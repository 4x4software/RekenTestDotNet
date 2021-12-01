using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RekenTest.Common.Implementers;
using RekenTest.Common.Interfaces;

namespace RekenTest.Common.Tests
{
    public class ProblemTests
    {
        private IProblem ClassSUT = null;

        [SetUp]
        public void Setup()
        {
            ClassSUT = new Problem();
        }

        [Test]
        public void AssignValues_ShouldUseParameters ()
        { 
        }

        [TestCase("1", "")]
        [TestCase("1+", "")]
        [TestCase("+1", "")]
        [TestCase("+", "")]
        [TestCase("1++1", "")]

        [TestCase("1-", "")]
        [TestCase("-+1", "")]
        [TestCase("-", "")]
        [TestCase("1--1", "")]

        [TestCase("1*", "")]
        [TestCase("*1", "")]
        [TestCase("*", "")]
        [TestCase("1**1", "")]

        [TestCase("1/", "")]
        [TestCase("/1", "")]
        [TestCase("/", "")]
        [TestCase("1//1", "")]
        [Test]
        public void ParseFromString_InvalidInputString_ShouldReturnFalse(string value, string message)
        {
            Assert.IsFalse(ClassSUT.ParseFromString(value), message);
        }
    }
}
