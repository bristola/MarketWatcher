using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowProcessor.contracts
{
    public interface IWorkflowActionProcessorFactory
    {
        IWorkflowActionProcessor Create(string actionType);
    }
}
