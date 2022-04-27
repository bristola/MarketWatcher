using Data.data;
using DataAccess.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.services
{
    public class WorkflowDataService : IWorkflowDataService
    {
        private readonly IWorkflowQueries _queries;
        private readonly IWorkflowCommands _commands;

        public WorkflowDataService(IWorkflowQueries queries, IWorkflowCommands commands)
        {
            _queries = queries;
            _commands = commands;
        }
        public ConditionDTO GetConditions(int workflowActionId)
        {
            return _queries.GetConditions(workflowActionId);
        }

        public decimal GetMarketData(string productTypeCode, string marketTypeCode, int minutesAgo)
        {
            return _queries.GetMarketData(productTypeCode, marketTypeCode, minutesAgo);
        }

        public List<WorkflowDTO> GetWorkflows(int page, int limit)
        {
            return _queries.GetWorkflows(page, limit);
        }

        public void SaveWorkflow(WorkflowDTO workflow)
        {
            _commands.SaveWorkflow(workflow);
        }

        public void SaveConditionWorkflowAction(ConditionWorkflowActionDTO conditionWorkflowAction)
        {
            var workflowActionId = _commands.AddWorkflowAction(conditionWorkflowAction.WorkflowId, conditionWorkflowAction.WorkflowAction);
            _commands.SaveConditionWorkflowAction(workflowActionId, conditionWorkflowAction.Condition);
        }
    }
}
