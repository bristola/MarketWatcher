using Data.context;
using DataAccess.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MarketDataCommands : IMarketDataCommands
    {
        private readonly MarketContext _context;

        public MarketDataCommands(MarketContext context)
        {
            _context = context;
        }

        public void InsertDataEntry(MarketDataEntry dataEntry)
        {
            dataEntry.Product = _context.Products.Single(p => p.Code == dataEntry.Product.Code);

            var types = _context.MarketDataTypes.ToList();
            foreach (var value in dataEntry.MarketDataValues)
            {
                value.MarketDataType = types.Single(t => t.Code == value.MarketDataType.Code);
            }

            _context.MarketDataEntries.Add(dataEntry);
            _context.SaveChanges();
        }

        public void ExpireDataEntries(int minutes)
        {
            var minutesAgo = DateTime.UtcNow.AddMinutes(-1 * minutes);
            var expiredEntries = _context.MarketDataEntries.Where(entry => entry.TimeStamp < minutesAgo);
            _context.MarketDataEntries.RemoveRange(expiredEntries);
            _context.SaveChanges();
        }
    }
}
