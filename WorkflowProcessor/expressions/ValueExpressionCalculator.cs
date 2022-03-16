using Data.context;
using DataAccess.contracts;
using WorkflowProcessor.contracts;

namespace WorkflowProcessor.expressions
{
    public class ValueExpressionCalculator : AbstractExpressionValidator
    {
        public ValueExpressionCalculator(IWorkflowQueries workflowQueries) : base(workflowQueries)
        {
        }

        public override decimal Calculate(Expression expression)
        {
            return GetMarketData(expression.ProductOne.Code, expression.MarketDataTypeOne.Code);
        }

    }
}