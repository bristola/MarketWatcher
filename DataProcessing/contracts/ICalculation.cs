using Coinbase.Pro.Models;
using Data.context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.contracts
{
    public interface ICalculation<T> where T : Event
    {
        MarketDataEntry Execute(T ev);
    }
}
