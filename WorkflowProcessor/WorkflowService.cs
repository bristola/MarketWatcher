using System;
using System.Collections.Generic;
using System.Text;
using Data.context;
using Data.data;
using DataAccess.contracts;
using WorkflowProcessor.contracts;

namespace WorkflowProcessor
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IWorkflowDataService _workflowDataService;
        private readonly IWorkflowActionProcessorFactory _processorFactory;

        public WorkflowService(IWorkflowDataService workflowDataService, IWorkflowActionProcessorFactory processorFactory)
        {
            _workflowDataService = workflowDataService;
            _processorFactory = processorFactory;
        }

        public List<WorkflowDTO> GetWorkflows(int page, int limit)
        {
            return _workflowDataService.GetWorkflows(page, limit);
        }

        public void Execute(WorkflowDTO workflow)
        {
            var currentAction = workflow.CurrentWorkflowAction;
            var actionProcessor = _processorFactory.Create(currentAction.WorkflowActionType.Code);
            var completed = actionProcessor.Process(currentAction);
            // If completed move on to next step
            // If completed last step, check if current iteration is final iteration, if so change status
        }
    }
}
