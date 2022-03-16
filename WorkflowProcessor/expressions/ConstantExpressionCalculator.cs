using Data.context;
using DataAccess.contracts;
using WorkflowProcessor.contracts;

namespace WorkflowProcessor.expressions
{
    public class ConstantExpressionCalculator : AbstractExpressionValidator
    {
        public ConstantExpressionCalculator(IWorkflowQueries workflowQueries) : base(workflowQueries)
        {
        }

        public override decimal Calculate(Expression value)
        {
            throw new NotImplementedException();
        }

    }
}