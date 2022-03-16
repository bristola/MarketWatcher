using System;
using System.Collections.Generic;
using System.Text;
using Data.context;
using WorkFlowProcessor.contracts;

namespace WorkFlowProcessor
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IWorkflowActionProcessorFactory _processorFactory;

        public WorkflowService(IWorkflowActionProcessorFactory processorFactory)
        {
            _processorFactory = processorFactory;
        }

        public void Execute(Workflow workflow)
        {
            var currentAction = workflow.CurrentWorkflowAction;
            var actionProcessor = _processorFactory.Create(currentAction.WorkflowActionType.Code);
            var completed = actionProcessor.Process(currentAction);
            // If completed move on to next step
        }
    }
}
