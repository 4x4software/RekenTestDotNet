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
            ClassSUT = new Problem(new ProblemValueFactory());
        }

        [Test]
        public void AssignValues_ShouldUseParameters ()
        { 
        }

        [TestCase("1", "")]
        [TestCase("1+", "")]
        [TestCase("+1", "")]
        [TestCase("+", "")]

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
        public void ParseFromString_InvalidInputString_ShouldReturnFalse(string value, string message)
        {
            Assert.IsFalse(ClassSUT.ParseFromString(value), message);
        }

        [TestCase("1++1", "+1 parses as 1")]
        public void ParseFromString_ValidInputString_ShouldReturnTrue(string value, string message)
        {
            Assert.IsTrue(ClassSUT.ParseFromString(value), message);
        }
        
        [TestCase("1+2", ProblemType.ptAdd, "1", "2")]
        [TestCase("2-1", ProblemType.ptSubtract, "2", "1")]
        [TestCase("3*2", ProblemType.ptMultiply, "3", "2")]
        [TestCase("6/2", ProblemType.ptDivide, "6", "2")]
        public void ParseFromString_BasicTests(string actual, ProblemType expectedType, string expectedValueA, string expectedValueB)
        {
            Assert.IsTrue(ClassSUT.ParseFromString(actual), actual);
            
            
        }
    }
}
