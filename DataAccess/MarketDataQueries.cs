using Data.constants;
using Data.context;
using DataAccess.contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class MarketDataQueries : IMarketDataQueries
    {
        private readonly MarketContext _context;

        public MarketDataQueries(MarketContext context)
        {
            _context = context;
        }

        public List<MarketDataEntry> GetPreviousMarketData(string productCode, int minutesAgo)
        {
            return _context.MarketDataEntries
                .Include(entry => entry.MarketDataValues)
                .ThenInclude(value => value.MarketDataType)
                .Where(entry => entry.Product.Code == productCode)
                .Where(entry => entry.TimeStamp > DateTime.UtcNow.AddMinutes(-1 * minutesAgo))
                .ToList();
        }

        public List<Product> GetProducts(string productTypeCode)
        {
            return _context.Products
                .Include(p => p.ProductType)
                .Where(p => p.ProductType.Code == productTypeCode)
                .ToList();
        }

        public List<Workflow> GetWorkflows(int userId, int page, int limit)
        {
            return _context.Workflows
                .Skip(page * limit)
                .Take(limit)
                .ToList();
        }
    }
}
