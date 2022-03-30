using Data.context;
using Data.data;
using System.Collections.Generic;

namespace DataAccess.contracts
{
    public interface IWorkflowQueries
    {
        List<WorkflowDTO> GetWorkflows(int page, int limit);
        ConditionDTO GetConditions(int workflowActionId);
        decimal GetMarketData(string productTypeCode, string marketTypeCode, int minutesAgo);
    }
}