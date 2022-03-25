﻿using Data.constants;
using Data.context;
using DataAccess.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowProcessor.contracts;

namespace WorkflowProcessor.expressions
{
    public class ExpressionCalculator : IExpressionCalculator
    {
        private readonly IWorkflowQueries _workflowQueries;

        public ExpressionCalculator(IWorkflowQueries workflowQueries)
        {
            _workflowQueries = workflowQueries;
        }

        public decimal Calculate(List<ConditionToken> tokens)
        {
            // TODO: Make tests for this to actually test it out

            // Parenthesis
            var currentIndex = tokens.FindIndex(t => t.ConditionTokenType.ConstantValue == "(");
            while (currentIndex >= 0)
            {
                var closeIndex = tokens.FindIndex(t => t.ConditionTokenType.ConstantValue == ")");
                var parenthesisValue = Calculate(tokens.GetRange(currentIndex + 1, closeIndex - currentIndex - 1));
                tokens.RemoveRange(currentIndex, closeIndex - currentIndex + 1);
                tokens.Insert(currentIndex, GetConstantValueToken(parenthesisValue));
                currentIndex = tokens.FindIndex(t => t.ConditionTokenType.ConstantValue == "(");
            }

            // Multiplication and division
            currentIndex = tokens.FindIndex(t => t.ConditionTokenType.ConstantValue == "*" || t.ConditionTokenType.ConstantValue == "/");
            while (currentIndex >= 0)
            {
                var val1 = GetValue(tokens.ElementAt(currentIndex - 1));
                var val2 = GetValue(tokens.ElementAt(currentIndex + 1));
                var op = tokens.ElementAt(currentIndex);
                var res = op.ConditionTokenType.ConstantValue == "*" ? val1 * val2 : val1 / val2;
                tokens.RemoveRange(currentIndex - 1, 3);
                tokens.Insert(currentIndex - 1, GetConstantValueToken(res));
                currentIndex = tokens.FindIndex(t => t.ConditionTokenType.ConstantValue == "*" || t.ConditionTokenType.ConstantValue == "/");
            }

            // Addition and subtraction
            // Multiplication and division
            currentIndex = tokens.FindIndex(t => t.ConditionTokenType.ConstantValue == "+" || t.ConditionTokenType.ConstantValue == "-");
            while (currentIndex >= 0)
            {
                var val1 = GetValue(tokens.ElementAt(currentIndex - 1));
                var val2 = GetValue(tokens.ElementAt(currentIndex + 1));
                var op = tokens.ElementAt(currentIndex);
                var res = op.ConditionTokenType.ConstantValue == "+" ? val1 + val2 : val1 - val2;
                tokens.RemoveRange(currentIndex - 1, 3);
                tokens.Insert(currentIndex - 1, GetConstantValueToken(res));
                currentIndex = tokens.FindIndex(t => t.ConditionTokenType.ConstantValue == "+" || t.ConditionTokenType.ConstantValue == "-");
            }

            var result = tokens.First().ConstantValue;

            if (tokens.Count > 1 || result == null)
            {
                throw new Exception("Calculation of tokens failed");
            }

            return (decimal) result;
        }

        private ConditionToken GetConstantValueToken(decimal? value)
        {
            return new ConditionToken
            {
                ConstantValue = value,
                ConditionTokenType = new ConditionTokenType
                {
                    Code = WorkflowConstants.ConditionTokenTypes.Constant
                }
            };
        }

        private decimal? GetValue(ConditionToken token)
        {
            switch (token.ConditionTokenType.Code)
            {
                case WorkflowConstants.ConditionTokenTypes.Constant:
                    return token.ConstantValue;
                case WorkflowConstants.ConditionTokenTypes.MarketValue:
                    return _workflowQueries.GetMarketData(token.Product.Code, token.MarketDataType.Code, 15);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
