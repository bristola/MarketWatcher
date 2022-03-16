using Data.context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.contracts
{
    public interface IMarketDataQueries
    {
        List<MarketDataEntry> GetPreviousMarketData(string productCode, int minutesAgo);
        List<Product> GetProducts(string productTypeCode);
    }
}
