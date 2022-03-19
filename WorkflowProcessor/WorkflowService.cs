using System;
using System.Collections.Generic;
using System.Text;
using Data.context;
using DataAccess.contracts;
using WorkflowProcessor.contracts;

namespace WorkflowProcessor
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IWorkflowQueries _workflowQueries;
        private readonly IWorkflowActionProcessorFactory _processorFactory;

        public WorkflowService(IWorkflowQueries workflowQueries, IWorkflowActionProcessorFactory processorFactory)
        {
            _workflowQueries = workflowQueries;
            _processorFactory = processorFactory;
        }

        public List<Workflow> GetWorkflows(int page, int limit)
        {
            return _workflowQueries.GetWorkflows(page, limit);
        }

        public void Execute(Workflow workflow)
        {
            var currentAction = workflow.CurrentWorkflowAction;
            var actionProcessor = _processorFactory.Create(currentAction.WorkflowActionType.Code);
            var completed = actionProcessor.Process(currentAction);
            // If completed move on to next step
            // If completed last step, check if current iteration is final iteration, if so change status
        }
    }
}
