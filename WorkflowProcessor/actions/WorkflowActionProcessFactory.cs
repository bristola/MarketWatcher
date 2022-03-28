using Data.constants;
using DataAccess.contracts;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowProcessor.actions;
using WorkflowProcessor.contracts;

namespace WorkflowProcessor.actions
{
    public class WorkflowActionProcessFactory : IWorkflowActionProcessorFactory
    {
        private readonly IWorkflowQueries _workflowQueries;
        private readonly IConditionValidatorFactory _conditionValidatorFactory;
        private readonly IExpressionCalculator _expressionCalculator;

        public WorkflowActionProcessFactory(IWorkflowQueries workflowQueries, IConditionValidatorFactory conditionValidatorFactory, IExpressionCalculator expressionCalculator)
        {
            _workflowQueries = workflowQueries;
            _conditionValidatorFactory = conditionValidatorFactory;
            _expressionCalculator = expressionCalculator;
        }

        public IWorkflowActionProcessor Create(string actionType)
        {
            switch (actionType)
            {
                case WorkflowConstants.Actions.Condition:
                    return new ConditionalActionProcessor(_workflowQueries, _conditionValidatorFactory, _expressionCalculator);
                case WorkflowConstants.Actions.Timer:
                    return new TimerActionProcessor();
                default:
                    throw new Exception($"Invalid action type: {actionType}");
            }
        }
    }
}
