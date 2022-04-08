using Data.context;
using Data.data;
using DataAccess.contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace DataAccess
{
    public class WorkflowQueries : IWorkflowQueries
    {
        private readonly MarketContext _context;
        private readonly IMapper _mapper;

        public WorkflowQueries(MarketContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<WorkflowDTO> GetWorkflows(int page, int limit)
        {
            var workflows = _context.Workflows
                .Include(w => w.CurrentWorkflowAction)
                .ThenInclude(w => w.WorkflowActionType)
                .Include(w => w.CurrentWorkflowAction)
                .ThenInclude(w => w.NextWorkFlowAction)
                .AsNoTracking()
                .Skip(page * limit)
                .Take(limit)
                .ToList();

            return _mapper.Map<List<WorkflowDTO>>(workflows);
        }

        public ConditionDTO GetConditions(int workflowActionId)
        {
            var condition = _context.Conditions
                .Include(c => c.ConditionType)
                .Include(c => c.Tokens)
                .ThenInclude(c => c.ConditionTokenType)
                .Include(c => c.Tokens)
                .ThenInclude(c => c.Product)
                .Include(c => c.Tokens)
                .ThenInclude(c => c.MarketDataType)
                .AsNoTracking()
                .Where(c => c.WorkflowActionId == workflowActionId)
                .Single();

            return _mapper.Map<ConditionDTO>(condition);
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
