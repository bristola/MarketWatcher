using Data.context;
using Data.data;
using DataAccess.contracts;
using WorkflowProcessor.contracts;

namespace WorkflowProcessor.actions
{
    public class ConditionalActionProcessor : IWorkflowActionProcessor
    {
        private readonly IWorkflowQueries _workflowQueries;
        private readonly IConditionValidatorFactory _conditionValidatorFactory;
        private readonly IExpressionCalculator _expressionCalculator;

        public ConditionalActionProcessor(IWorkflowQueries workflowQueries, IConditionValidatorFactory conditionValidatorFactory, IExpressionCalculator expressionCalculator)
        {
            _workflowQueries = workflowQueries;
            _conditionValidatorFactory = conditionValidatorFactory;
            _expressionCalculator = expressionCalculator;
        }

        public bool Process(WorkflowActionDTO action)
        {
            var condition = _workflowQueries.GetConditions(action.WorkflowActionId);

            var leftValue = _expressionCalculator.Calculate(condition.LeftTokens);
            var rightValue = _expressionCalculator.Calculate(condition.RightTokens);

            return Validate(condition.ConditionType.Code, leftValue, rightValue);
        }

        private bool Validate(string conditionType, decimal leftValue, decimal rightValue)
        {
            var validator = _conditionValidatorFactory.Create(conditionType);

            return validator.Validate(leftValue, rightValue);
        }
    }
}
