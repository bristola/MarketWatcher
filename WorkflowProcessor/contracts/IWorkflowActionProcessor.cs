using Data.context;

namespace WorkflowProcessor.contracts
{
    public interface IWorkflowActionProcessor
    {
        bool Process(WorkflowAction action);
    }
}