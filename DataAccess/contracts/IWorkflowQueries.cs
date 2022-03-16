using Data.context;
using System.Collections.Generic;

namespace DataAccess.contracts
{
    public interface IWorkflowQueries
    {
        List<Workflow> GetWorkflows(int page, int limit);
        Condition GetConditions(int workflowActionId);
        decimal GetMarketData(string productTypeCode, string marketTypeCode, int minutesAgo);
    }
}