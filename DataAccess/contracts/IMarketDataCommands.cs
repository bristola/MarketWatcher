using Data.context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.contracts
{
    public interface IMarketDataCommands
    {
        void InsertDataEntry(MarketDataEntry dataEntry);
        void ExpireDataEntries(int minutes);
    }
}
