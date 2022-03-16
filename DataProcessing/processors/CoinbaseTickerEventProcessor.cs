using System;
using System.Collections.Generic;
using System.Text;
using Coinbase.Pro.Models;
using Data.constants;
using Data.context;
using DataAccess.contracts;
using DataProcessing.calculations;

namespace DataProcessing.processors
{
    public class CoinbaseTickerEventProcessor : BaseProcessor<Event>
    {
        public CoinbaseTickerEventProcessor(IMarketDataQueries marketDataQueries, IMarketDataCommands marketDataCommands) : base(marketDataQueries, marketDataCommands)
        {
        }

        protected override MarketDataEntry CreateMarketDataEntry(Event data)
        {
            var tickerEvent = data as TickerEvent;

            var marketDataValues = new List<MarketDataValue>();
            if (tickerEvent?.BestAsk != null)
            {
                marketDataValues.Add(new MarketDataValue
                {
                    MarketDataType = new MarketDataType
                    {
                        Code = MarketDataTypeConstants.AskPrice
                    },
                    Value = (decimal)tickerEvent.BestAsk
                });
            }
            if (tickerEvent?.BestBid != null)
            {
                marketDataValues.Add(new MarketDataValue
                {
                    MarketDataType = new MarketDataType
                    {
                        Code = MarketDataTypeConstants.BidPrice
                    },
                    Value = (decimal)tickerEvent.BestBid
                });
            }

            if (tickerEvent?.BestBid != null && tickerEvent?.BestAsk != null)
            {
                marketDataValues.Add(new MarketDataValue
                {
                    MarketDataType = new MarketDataType
                    {
                        Code = MarketDataTypeConstants.BidAskSpread
                    },
                    Value = (decimal)tickerEvent.BestBid - (decimal)tickerEvent.BestAsk
                });
            }

            return new MarketDataEntry
            {
                TimeStamp = DateTime.UtcNow,
                Product = new Data.context.Product
                {
                    Code = tickerEvent.ProductId
                },
                MarketDataValues = marketDataValues
            };
        }

        protected override MarketDataEntry Calculate(MarketDataEntry currentMarketData, List<MarketDataEntry> previousMarketData)
        {
            var calculation = new ValueChangeCalculation(MarketDataTypeConstants.BidPrice, MarketDataTypeConstants.FiveMinutePriceChange, 5);
            calculation.SetNext(new ValueChangeCalculation(MarketDataTypeConstants.BidPrice, MarketDataTypeConstants.FifteenMinutePriceChange, 15))
                .SetNext(new BidAskSpreadCalculation());

            return calculation.Execute(currentMarketData, previousMarketData);
        }
    }
}
