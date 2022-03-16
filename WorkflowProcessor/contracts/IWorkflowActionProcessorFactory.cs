using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowProcessor.contracts
{
    public interface IWorkflowActionProcessorFactory
    {
        IWorkflowActionProcessor Create(string actionType);
    }
}
