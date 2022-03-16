using Data.context;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowProcessor.contracts
{
    public interface IWorkflowService
    {
        void Execute(Workflow workflow);
    }
}
