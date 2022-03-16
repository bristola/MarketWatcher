using Data.context;
using DataAccess.contracts;
using WorkflowProcessor.contracts;

namespace WorkflowProcessor.expressions
{
    internal class AdditionExpressionCalculator : AbstractExpressionValidator
    {
        public AdditionExpressionCalculator(IWorkflowQueries workflowQueries) : base(workflowQueries)
        {
        }

        public override decimal Calculate(Expression expression)
        {
            var valueOne = GetMarketData(expression.ProductOne.Code, expression.MarketDataTypeOne.Code);
            var valueTwo = GetMarketData(expression.ProductTwo.Code, expression.MarketDataTypeTwo.Code);

            return valueOne + valueTwo;
        }
    }
}