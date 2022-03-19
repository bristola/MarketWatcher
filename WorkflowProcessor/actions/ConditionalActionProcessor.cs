﻿using Data.context;
using DataAccess.contracts;
using WorkflowProcessor.contracts;

namespace WorkflowProcessor.actions
{
    public class ConditionalActionProcessor : IWorkflowActionProcessor
    {
        private readonly IWorkflowQueries _workflowQueries;
        private readonly IConditionValidatorFactory _conditionValidatorFactory;
        private readonly IExpressionCalculatorFactory _expressionCalculatorFactory;

        public ConditionalActionProcessor(IWorkflowQueries workflowQueries, IConditionValidatorFactory conditionValidatorFactory, IExpressionCalculatorFactory expressionCalculatorFactory)
        {
            _workflowQueries = workflowQueries;
            _conditionValidatorFactory = conditionValidatorFactory;
            _expressionCalculatorFactory = expressionCalculatorFactory;
        }

        public bool Process(WorkflowAction action)
        {
            var condition = _workflowQueries.GetConditions(action.Id);

            var leftValue = GetValue(condition.LeftExpression);
            var rightValue = GetValue(condition.RightExpression);

            return Validate(condition.ConditionType.Code, leftValue, rightValue);
        }

        private decimal GetValue(Expression expression)
        {
            var calculator = _expressionCalculatorFactory.Create(expression.ExpressionType.Code);

            return calculator.Calculate(expression);
        }

        private bool Validate(string conditionType, decimal leftValue, decimal rightValue)
        {
            var validator = _conditionValidatorFactory.Create(conditionType);

            return validator.Validate(leftValue, rightValue);
        }
    }
}
