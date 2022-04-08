using Data.context;
using Data.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowProcessor.contracts
{
    public interface IWorkflowService
    {
        List<WorkflowDTO> GetWorkflows(int page, int limit);
        void Execute(WorkflowDTO workflow);
    }
}
