using Data.context;
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

        public bool Process(WorkflowAction action)
        {
            var condition = _workflowQueries.GetConditions(action.Id);

            var leftValue = _expressionCalculator.Calculate(GetLeftTokens(condition));
            var rightValue = _expressionCalculator.Calculate(GetRightTokens(condition));

            return Validate(condition.ConditionType.Code, leftValue, rightValue);
        }

        private List<ConditionToken> GetLeftTokens(Condition condition) => condition.Tokens.Where(t => t.IsLeftExpression).ToList();

        private List<ConditionToken> GetRightTokens(Condition condition) => condition.Tokens.Where(t => !t.IsLeftExpression).ToList();

        private bool Validate(string conditionType, decimal leftValue, decimal rightValue)
        {
            var validator = _conditionValidatorFactory.Create(conditionType);

            return validator.Validate(leftValue, rightValue);
        }
    }
}
