using Data.context;

namespace WorkFlowProcessor.contracts
{
    public interface IWorkflowActionProcessor
    {
        bool Process(WorkflowAction action);
    }
}