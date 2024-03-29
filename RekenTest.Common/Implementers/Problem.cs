﻿using RekenTest.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RekenTest.Common.Implementers
{
    public class Problem : IProblem
    {
        private IProblemValue _problemValueA;
        private IProblemValue _problemValueB;
        private ProblemType _problemType;
        private IProblemValue _correctAnswer;

        public IProblemValue ValueA => _problemValueA;
        public IProblemValue ValueB => _problemValueB;
        public ProblemType Type => _problemType;

        public Problem(IProblemValueFactory problemValueFactory)
        {
            _problemValueA = problemValueFactory.NewProblemValue();
            _problemValueB = problemValueFactory.NewProblemValue();
            _correctAnswer = problemValueFactory.NewProblemValue();
        }

        public void AssignValues(ProblemType problemType, IProblemValue problemValueA, IProblemValue problemValueB)
        {
            _problemType = problemType;
            _problemValueA = problemValueA;
            _problemValueB = problemValueB;
        }

        public IProblemValue GetCorrectAnswer()
        {
            if (ProblemCalculator.CalculateCorrectAnswer(_problemType, _problemValueA, _problemValueB, _correctAnswer))
                return _correctAnswer;

            return null;
        }

        public bool IsValid()
        {
            if (GetCorrectAnswer() == null)
                return false;

            return (_problemValueA.IsValid() && _problemValueB.IsValid() && _correctAnswer.IsValid());
        }

        public bool ParseFromString(string problemAsText)
        {
            int indexSymbol = GetIndexOfProblemSymbol(problemAsText, out ProblemType? problemType);

            if (indexSymbol > 0)
            {
                _problemType = (ProblemType)problemType;
                if (!_problemValueA.ParseFromString(problemAsText.Substring(0, indexSymbol)))
                    return false;
                if (!_problemValueB.ParseFromString(problemAsText.Substring(indexSymbol + 1)))
                    return false;

                return IsValid();
            }

            return false;
        }

        private int GetIndexOfProblemSymbol(string problemAsText, out ProblemType? problemType)
        {
            problemType = null;
            int indexSymbol = problemAsText.IndexOf(ProblemValueTypes.ProblemTypeAddSymbol);


            if (indexSymbol >= 0)
                problemType = ProblemType.ptAdd;
            else
            {
                indexSymbol = problemAsText.IndexOf(ProblemValueTypes.ProblemTypeSubtractSymbol);
                if (indexSymbol >= 0)
                    problemType = ProblemType.ptSubtract;
                else
                {
                    indexSymbol = problemAsText.IndexOf(ProblemValueTypes.ProblemTypeMultiplySymbol);
                    if (indexSymbol >= 0)
                        problemType = ProblemType.ptMultiply;
                    else
                    {
                        indexSymbol = problemAsText.IndexOf(ProblemValueTypes.ProblemTypeDivideSymbol);

                        if (indexSymbol >= 0)
                            problemType = ProblemType.ptDivide;
                    }
                }
            }

            return indexSymbol;
        }
    }
}