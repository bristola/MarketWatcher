using Coinbase.Pro.Models;
using Data.constants;
using DataAccess.contracts;
using System;
using System.Collections.Generic;
using System.Text;
using DataProcessing.contracts;
using DataProcessing.processors;

namespace DataProcessing.helpers
{
    public class MessageProcessorFactory : IMessageProcessorFactory
    {
        private readonly IMarketDataQueries _marketDataQueries;
        private readonly IMarketDataCommands _marketDataCommands;
        public MessageProcessorFactory(IMarketDataQueries marketDataQueries, IMarketDataCommands marketDataCommands)
        {
            _marketDataQueries = marketDataQueries;
            _marketDataCommands = marketDataCommands;
        }

        public IMessageProcessor<Event> Create(string type)
        {
            switch (type)
            {
                case WebSocketConstants.Channels.Snapshot:
                    return new CoinbaseSnapshotProcessor(_marketDataQueries, _marketDataCommands);
                case WebSocketConstants.Channels.Ticker:
                    return new CoinbaseTickerEventProcessor(_marketDataQueries, _marketDataCommands);
                default:
                    return null;
            }
        }
    }
}
