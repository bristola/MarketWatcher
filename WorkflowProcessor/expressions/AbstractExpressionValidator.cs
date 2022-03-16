using Data.context;
using DataAccess.contracts;
using WorkflowProcessor.contracts;

namespace WorkflowProcessor.expressions
{
    public abstract class AbstractExpressionValidator : IExpressionCalculator
    {
        private readonly IWorkflowQueries _workflowQueries;

        public AbstractExpressionValidator(IWorkflowQueries workflowQueries)
        {
            _workflowQueries = workflowQueries;
        }

        public abstract decimal Calculate(Expression value);

        protected decimal GetMarketData(string productTypeCode, string marketTypeCode)
        {
            return _workflowQueries.GetMarketData(productTypeCode, marketTypeCode, 5);
        }
    }
}