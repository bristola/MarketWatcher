using Data.context;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowProcessor.contracts
{
    public interface IWorkflowService
    {
        List<Workflow> GetWorkflows(int page, int limit);
        void Execute(Workflow workflow);
    }
}
