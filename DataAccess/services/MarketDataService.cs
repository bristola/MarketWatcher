using Data.context;
using DataAccess.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.services
{
    public class MarketDataService : IMarketDataService
    {
        private readonly IMarketDataQueries _queries;
        private readonly IMarketDataCommands _commands;

        public MarketDataService(IMarketDataQueries queries, IMarketDataCommands commands)
        {
            _queries = queries;
            _commands = commands;
        }
        public void ExpireDataEntries(int minutes)
        {
            _commands.ExpireDataEntries(minutes);
        }

        public List<MarketDataEntry> GetPreviousMarketData(string productCode, int minutesAgo)
        {
            return _queries.GetPreviousMarketData(productCode, minutesAgo);
        }

        public List<Product> GetProducts(string productTypeCode)
        {
            return _queries.GetProducts(productTypeCode);
        }

        public void InsertDataEntry(MarketDataEntry dataEntry)
        {
            _commands.InsertDataEntry(dataEntry);
        }
    }
}
