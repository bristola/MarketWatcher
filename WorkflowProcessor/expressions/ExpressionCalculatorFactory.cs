using Data.constants;
using DataAccess.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowProcessor.contracts;

namespace WorkflowProcessor.expressions
{
    public class ExpressionCalculatorFactory : IExpressionCalculatorFactory
    {
        private readonly IWorkflowQueries _workflowQueries;

        public ExpressionCalculatorFactory(IWorkflowQueries workflowQueries)
        {
            _workflowQueries = workflowQueries;
        }

        public IExpressionCalculator Create(string expressionTypeCode)
        {
            switch (expressionTypeCode)
            {
                case WorkflowConstants.ExpressionTypes.Addition:
                    return new AdditionExpressionCalculator(_workflowQueries);

                case WorkflowConstants.ExpressionTypes.Subtraction:
                    return new SubtractionExpressionCalculator(_workflowQueries);

                case WorkflowConstants.ExpressionTypes.Division:
                    return new DivisionExpressionCalculator(_workflowQueries);

                case WorkflowConstants.ExpressionTypes.Multiply:
                    return new MultiplyExpressionCalculator(_workflowQueries);

                case WorkflowConstants.ExpressionTypes.Value:
                    return new ValueExpressionCalculator(_workflowQueries);

                case WorkflowConstants.ExpressionTypes.Constant:
                    return new ConstantExpressionCalculator(_workflowQueries);

                default:
                    throw new Exception($"Invalid expression type: {expressionTypeCode}");
            }
        }
    }
}
