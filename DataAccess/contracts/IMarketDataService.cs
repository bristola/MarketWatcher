using Data.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.contracts
{
    public interface IMarketDataService
    {
        void InsertDataEntry(MarketDataEntry dataEntry);
        void ExpireDataEntries(int minutes);
        List<MarketDataEntry> GetPreviousMarketData(string productCode, int minutesAgo);
        List<Product> GetProducts(string productTypeCode);
    }
}
