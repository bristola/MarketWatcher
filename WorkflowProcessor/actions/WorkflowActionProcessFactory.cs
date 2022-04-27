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
        private readonly IWorkflowDataService _workflowDataService;
        private readonly IConditionValidatorFactory _conditionValidatorFactory;
        private readonly IExpressionCalculator _expressionCalculator;

        public WorkflowActionProcessFactory(IWorkflowDataService workflowDataService, IConditionValidatorFactory conditionValidatorFactory, IExpressionCalculator expressionCalculator)
        {
            _workflowDataService = workflowDataService;
            _conditionValidatorFactory = conditionValidatorFactory;
            _expressionCalculator = expressionCalculator;
        }

        public IWorkflowActionProcessor Create(string actionType)
        {
            switch (actionType)
            {
                case WorkflowConstants.Actions.Condition:
                    return new ConditionalActionProcessor(_workflowDataService, _conditionValidatorFactory, _expressionCalculator);
                case WorkflowConstants.Actions.Timer:
                    return new TimerActionProcessor();
                default:
                    throw new Exception($"Invalid action type: {actionType}");
            }
        }
    }
}
