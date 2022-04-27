using Data.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.contracts
{
    public interface IWorkflowCommands
    {
        void SaveWorkflow(WorkflowDTO workflow);
        int AddWorkflowAction(int workflowId, WorkflowActionDTO workflowAction);
        void SaveConditionWorkflowAction(int workflowActionId, ConditionDTO condition);
    }
}
