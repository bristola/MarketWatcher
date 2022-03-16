using Data.constants;
using DataAccess.contracts;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowProcessor.contracts;
using WorkFlowProcessor.actions;
using WorkFlowProcessor.contracts;

namespace WorkFlowProcessor
{
    public class WorkflowActionProcessFactory : IWorkflowActionProcessorFactory
    {
        private readonly IWorkflowQueries _workflowQueries;
        private readonly IConditionValidatorFactory _conditionValidatorFactory;
        private readonly IExpressionCalculatorFactory _expressionCalculatorFactory;

        public WorkflowActionProcessFactory(IWorkflowQueries workflowQueries, IConditionValidatorFactory conditionValidatorFactory, IExpressionCalculatorFactory expressionCalculatorFactory)
        {
            _workflowQueries = workflowQueries;
            _conditionValidatorFactory = conditionValidatorFactory;
            _expressionCalculatorFactory = expressionCalculatorFactory;
        }

        public IWorkflowActionProcessor Create(string actionType)
        {
            switch (actionType)
            {
                case WorkflowConstants.Actions.Condition:
                    return new ConditionalActionProcessor(_workflowQueries, _conditionValidatorFactory, _expressionCalculatorFactory);
                default:
                    throw new Exception($"Invalid action type: {actionType}");
            }
        }
    }
}
