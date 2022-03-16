using Data.context;
using DataAccess.contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class WorkflowQueries : IWorkflowQueries
    {
        private readonly MarketContext _context;

        public WorkflowQueries(MarketContext context)
        {
            _context = context;
        }

        public List<Workflow> GetWorkflows(int page, int limit)
        {
            return _context.Workflows
                .Include(w => w.CurrentWorkflowAction)
                .ThenInclude(w => w.WorkflowActionType)
                .Include(w => w.CurrentWorkflowAction)
                .ThenInclude(w => w.NextWorkFlowAction)
                .AsNoTracking()
                .Skip(page * limit)
                .Take(limit)
                .ToList();
        }

        public Condition GetConditions(int workflowActionId)
        {
            return _context.Conditions
                .Include(c => c.ConditionType)
                .AsNoTracking()
                .Where(c => c.WorkflowActionId == workflowActionId)
                .Single();
        }

        public decimal GetMarketData(string productTypeCode, string marketDataTypeCode, int minutesAgo)
        {
            var mostRecentEntry = _context.MarketDataEntries
                .Where(e => e.Product.Code == productTypeCode)
                .Where(e => e.TimeStamp > DateTime.UtcNow.AddMinutes(-1 * minutesAgo))
                .OrderByDescending(e => e.TimeStamp)
                .SelectMany(e => e.MarketDataValues)
                .Where(v => v.MarketDataType.Code == marketDataTypeCode)
                .FirstOrDefault();

            if (mostRecentEntry == null)
            {
                throw new Exception($"Could not find value for product {productTypeCode} and market data type {marketDataTypeCode} within the last {minutesAgo} minutes.");
            }

            return mostRecentEntry.Value;
        }
    }
}
