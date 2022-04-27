using Data.context;
using Data.data;
using DataAccess.contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class WorkflowCommands : IWorkflowCommands
    {
        private readonly MarketContext _context;

        public WorkflowCommands(MarketContext context)
        {
            _context = context;
        }

        public void SaveWorkflow(WorkflowDTO workflow)
        {
            _context.Add(new Workflow()
            {
                Name = workflow.Name,
                CreatedDate = DateTime.UtcNow,
                CurrentIteration = 1,
                Iterations = workflow.Iterations,
                WorkflowStatusId = workflow.WorkflowStatus.WorkflowStatusId,
            });
        }

        public int AddWorkflowAction(int workflowId, WorkflowActionDTO workflowAction)
        {
            var workflow = _context.Workflows
                .Include(w => w.WorkflowActions)
                .First(w => w.Id == workflowId);

            var action = new WorkflowAction
            {
                Name = workflowAction.Name,
                CreateDate = DateTime.UtcNow,
                WorkflowId = workflowId,
                WorkflowActionTypeId = workflowAction.WorkflowActionType.WorkflowActionTypeId
            };

            if (!workflow.WorkflowActions.Any())
            {
                // First workflow action
                workflow.CurrentWorkflowAction = action;
            }
            else if (workflowAction.NextWorkFlowActionId != null)
            {
                // In the middle
                var previousAction = workflow.WorkflowActions.Single(w => w.NextWorkFlowAction.Id == workflowAction.NextWorkFlowActionId);
                action.NextWorkFlowActionId = workflowAction.NextWorkFlowActionId;
                previousAction.NextWorkFlowAction = action;
            }
            else
            {
                // Just add to the end
                var previousAction = workflow.WorkflowActions.Last();
                previousAction.NextWorkFlowAction = action;
            }

            _context.SaveChanges();

            return action.Id;
        }

        public void SaveConditionWorkflowAction(int workflowActionId, ConditionDTO condition)
        {
            var tokens = condition.LeftTokens.Select(t => new ConditionToken
            {
                IsLeftExpression = true,
                ConditionTokenTypeId = t.ConditionTokenType.ConditionTokenTypeId,
                ConstantValue = t.ConstantValue,
                ProductId = t.Product?.ProductId,
                MarketDataTypeId = t.MarketDataType?.MarketDataTypeId
            }).ToList();
            tokens.AddRange(condition.RightTokens.Select(t => new ConditionToken
            {
                IsLeftExpression = false,
                ConditionTokenTypeId = t.ConditionTokenType.ConditionTokenTypeId,
                ConstantValue = t.ConstantValue,
                ProductId = t.Product?.ProductId,
                MarketDataTypeId = t.MarketDataType?.MarketDataTypeId
            }));

            _context.Conditions.Add(new Condition
            {
                WorkflowActionId = workflowActionId,
                Tokens = tokens,
                ConditionTypeId = condition.ConditionType.ConditionTypeId
            });

            _context.SaveChanges();
        }
    }
}
