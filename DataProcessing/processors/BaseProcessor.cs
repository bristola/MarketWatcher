using Coinbase.Pro.Models;
using Data.context;
using DataAccess.contracts;
using DataProcessing.contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.processors
{
    public abstract class BaseProcessor<T> : IMessageProcessor<T> where T : class
    {
        private readonly IMarketDataQueries _marketDataQueries;
        private readonly IMarketDataCommands _marketDataCommands;

        public BaseProcessor(IMarketDataQueries marketDataQueries, IMarketDataCommands marketDataCommands)
        {
            _marketDataQueries = marketDataQueries;
            _marketDataCommands = marketDataCommands;
        }

        public void Process(T data)
        {
            var currentMarketData = CreateMarketDataEntry(data);

            var previousMarketData = _marketDataQueries.GetPreviousMarketData(currentMarketData.Product.Code, 10);

            var calculated = Calculate(currentMarketData, previousMarketData);

            _marketDataCommands.InsertDataEntry(calculated);
        }

        protected abstract MarketDataEntry CreateMarketDataEntry(T data);

        protected abstract MarketDataEntry Calculate(MarketDataEntry currentMarketData, List<MarketDataEntry> previousMarketData);
    }
}
