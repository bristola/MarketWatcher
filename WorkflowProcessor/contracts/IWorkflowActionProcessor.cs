using Data.context;
using Data.data;

namespace WorkflowProcessor.contracts
{
    public interface IWorkflowActionProcessor
    {
        bool Process(WorkflowActionDTO action);
    }
}