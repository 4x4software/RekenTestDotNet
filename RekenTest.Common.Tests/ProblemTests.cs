﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RekenTest.Common.Implementers;
using RekenTest.Common.Interfaces;

namespace RekenTest.Common.Tests
{
    public class ProblemTests : ProblemTestBase
    {
        private IProblem _classSut = null;

        [SetUp]
        public void Setup()
        {
            _classSut = new Problem(new ProblemValueFactory());
        }

        [Test]
        public void AssignValues_ShouldUseParameters()
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
            Assert.IsFalse(_classSut.ParseFromString(value), message);
        }

        [TestCase("1++1", "+1 parses as 1")]
        public void ParseFromString_ValidInputString_ShouldReturnTrue(string value, string message)
        {
            Assert.IsTrue(_classSut.ParseFromString(value), message);
        }

        [TestCase("1+2", ProblemType.ptAdd, "1", "2")]
        [TestCase("2-1", ProblemType.ptSubtract, "2", "1")]
        [TestCase("3*2", ProblemType.ptMultiply, "3", "2")]
        [TestCase("6/2", ProblemType.ptDivide, "6", "2")]
        public void ParseFromString_BasicTests(string actual, ProblemType expectedType, string expectedValueA,
            string expectedValueB)
        {
            Assert.IsTrue(_classSut.ParseFromString(actual), actual);

            Assert.AreEqual(expectedType, _classSut.Type);
            Assert.IsTrue(_classSut.ValueA.IsEqualTo(problemValueFactory.NewProblemValue(expectedValueA)));
            Assert.IsTrue(_classSut.ValueB.IsEqualTo(problemValueFactory.NewProblemValue(expectedValueB)));
        }

        [TestCase("0.001+123.456", ProblemType.ptAdd, "0.001", "123.456")]
        [TestCase("1.234-1.233", ProblemType.ptSubtract, "1.234", "1.233")]
        [TestCase("0.5*20.2", ProblemType.ptMultiply, "0.5", "20.2")]
        [TestCase("0.6/20", ProblemType.ptDivide, "0.6", "20")]
        public void ParseFromString_DecimalValues(string actual, ProblemType expectedType, string expectedValueA,
            string expectedValueB)
        {
            Assert.IsTrue(_classSut.ParseFromString(actual), actual);

            Assert.AreEqual(expectedType, _classSut.Type);
            Assert.IsTrue(_classSut.ValueA.IsEqualTo(problemValueFactory.NewProblemValue(expectedValueA)));
            Assert.IsTrue(_classSut.ValueB.IsEqualTo(problemValueFactory.NewProblemValue(expectedValueB)));
        }

        [TestCase("999999+1.23456")]
        [TestCase("1.234-2.233")]
        [TestCase("0.00005*0.000005")]
        [TestCase("5/3")]
        public void ParseFromString_InvalidAnswers(string actual)
        {
            Assert.IsFalse(_classSut.ParseFromString(actual), actual);
        }

        [TestCase("  1 +  2  ", ProblemType.ptAdd, "1", "2")]
        [TestCase(" 2  -  1 ", ProblemType.ptSubtract, "2", "1")]
        [TestCase("  3 *2 ", ProblemType.ptMultiply, "3", "2")]
        [TestCase("6/ 2 ", ProblemType.ptDivide, "6", "2")]
        public void ParseFromString_WhiteSpace(string actual, ProblemType expectedType, string expectedValueA,
            string expectedValueB)
        {
            Assert.IsTrue(_classSut.ParseFromString(actual), actual);

            Assert.AreEqual(expectedType, _classSut.Type);
            Assert.IsTrue(_classSut.ValueA.IsEqualTo(problemValueFactory.NewProblemValue(expectedValueA)));
            Assert.IsTrue(_classSut.ValueB.IsEqualTo(problemValueFactory.NewProblemValue(expectedValueB)));
        }

        [Test]
        public void GetCorrectAnswer_IsValid_BasicTests()
        {
            Assert.IsTrue(_classSut.ParseFromString("10/5"));
            Assert.AreEqual(2, _classSut.GetCorrectAnswer().Value);
            Assert.AreEqual(0, _classSut.GetCorrectAnswer().Decimals);
            Assert.IsTrue(_classSut.IsValid());
            
            Assert.IsTrue(_classSut.ParseFromString("123+0.456"));
            Assert.AreEqual(123456, _classSut.GetCorrectAnswer().Value);
            Assert.AreEqual(3, _classSut.GetCorrectAnswer().Decimals);
            Assert.IsTrue(_classSut.IsValid());

            Assert.IsFalse(_classSut.ParseFromString("1/3"));
            Assert.IsNull(_classSut.GetCorrectAnswer());
            Assert.IsFalse(_classSut.IsValid());
        }
    }
}