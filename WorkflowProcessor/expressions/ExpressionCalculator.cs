using Data.constants;
using Data.context;
using DataAccess.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowProcessor.contracts;
using TokenTypes = Data.constants.WorkflowConstants.ConditionTokenTypes;

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
            int currentIndex = -1;

            // Parenthesis
            while ((currentIndex = FindTokenIndex(tokens, TokenTypes.OpenParenthesis)) >= 0)
            {
                var closeIndex = tokens.FindIndex(t => t.ConditionTokenType.Code == TokenTypes.CloseParenthesis);
                var parenthesisValue = Calculate(tokens.GetRange(currentIndex + 1, closeIndex - currentIndex - 1));
                tokens.RemoveRange(currentIndex, closeIndex - currentIndex + 1);
                tokens.Insert(currentIndex, GetConstantValueToken(parenthesisValue));
            }

            // Multiplication and division
            while ((currentIndex = FindTokenIndex(tokens, TokenTypes.Multiplication, TokenTypes.Division)) >= 0)
            {
                var val1 = GetValue(tokens.ElementAt(currentIndex - 1));
                var val2 = GetValue(tokens.ElementAt(currentIndex + 1));
                var op = tokens.ElementAt(currentIndex);
                var res = op.ConditionTokenType.Code == TokenTypes.Multiplication ? val1 * val2 : val1 / val2;
                tokens.RemoveRange(currentIndex - 1, 3);
                tokens.Insert(currentIndex - 1, GetConstantValueToken(res));
            }

            // Addition and subtraction
            while ((currentIndex = FindTokenIndex(tokens, TokenTypes.Addition, TokenTypes.Subtraction)) >= 0)
            {
                var val1 = GetValue(tokens.ElementAt(currentIndex - 1));
                var val2 = GetValue(tokens.ElementAt(currentIndex + 1));
                var op = tokens.ElementAt(currentIndex);
                var res = op.ConditionTokenType.Code == TokenTypes.Addition ? val1 + val2 : val1 - val2;
                tokens.RemoveRange(currentIndex - 1, 3);
                tokens.Insert(currentIndex - 1, GetConstantValueToken(res));
            }

            var result = tokens.First().ConstantValue;

            if (tokens.Count > 1 || result == null)
            {
                throw new Exception("Calculation of tokens failed");
            }

            return (decimal) result;
        }

        private int FindTokenIndex(List<ConditionToken> tokens, params string[] codes) =>
            tokens.FindIndex(t => codes.Contains(t.ConditionTokenType.Code));

        private ConditionToken GetConstantValueToken(decimal? value)
        {
            return new ConditionToken
            {
                ConstantValue = value,
                ConditionTokenType = new ConditionTokenType
                {
                    Code = TokenTypes.Constant
                }
            };
        }

        private decimal? GetValue(ConditionToken token)
        {
            switch (token.ConditionTokenType.Code)
            {
                case TokenTypes.Constant:
                    return token.ConstantValue;
                case TokenTypes.MarketValue:
                    return _workflowQueries.GetMarketData(token.Product.Code, token.MarketDataType.Code, token.MarketDataType.ExpirationMinutes);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
