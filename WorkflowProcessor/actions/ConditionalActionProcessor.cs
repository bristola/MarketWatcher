using Data.context;
using Data.data;
using DataAccess.contracts;
using WorkflowProcessor.contracts;

namespace WorkflowProcessor.actions
{
    public class ConditionalActionProcessor : IWorkflowActionProcessor
    {
        private readonly IWorkflowDataService _workflowDataService;
        private readonly IConditionValidatorFactory _conditionValidatorFactory;
        private readonly IExpressionCalculator _expressionCalculator;

        public ConditionalActionProcessor(IWorkflowDataService workflowDataService, IConditionValidatorFactory conditionValidatorFactory, IExpressionCalculator expressionCalculator)
        {
            _workflowDataService = workflowDataService;
            _conditionValidatorFactory = conditionValidatorFactory;
            _expressionCalculator = expressionCalculator;
        }

        public bool Process(WorkflowActionDTO action)
        {
            var condition = _workflowDataService.GetConditions(action.WorkflowActionId);

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
