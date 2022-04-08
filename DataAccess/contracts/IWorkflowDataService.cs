using Data.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.contracts
{
    public interface IWorkflowDataService
    {
        List<WorkflowDTO> GetWorkflows(int page, int limit);
        ConditionDTO GetConditions(int workflowActionId);
        decimal GetMarketData(string productTypeCode, string marketTypeCode, int minutesAgo);
        void SaveWorkflow(WorkflowDTO workflow);
    }
}
