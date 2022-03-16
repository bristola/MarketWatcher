using Coinbase.Pro.Models;
using Data.constants;
using Data.context;
using DataAccess.contracts;
using DataProcessing.calculations;
using DataProcessing.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataProcessing.processors
{
    public class CoinbaseSnapshotProcessor : BaseProcessor<Event>
    {
        public CoinbaseSnapshotProcessor(IMarketDataQueries marketDataQueries, IMarketDataCommands marketDataCommands) : base(marketDataQueries, marketDataCommands)
        {
        }

        protected override MarketDataEntry CreateMarketDataEntry(Event data)
        {
            var snapshotEvent = data as SnapshotEvent;

            var marketDataValues = new List<MarketDataValue>();
            if (snapshotEvent.Asks.Any())
            {
                marketDataValues.Add(new MarketDataValue
                {
                    MarketDataType = new MarketDataType
                    {
                        Code = MarketDataTypeConstants.AskPrice
                    },
                    Value = snapshotEvent.Asks.First().Price
                });
            }

            if (snapshotEvent.Bids.Any())
            {
                marketDataValues.Add(new MarketDataValue
                {
                    MarketDataType = new MarketDataType
                    {
                        Code = MarketDataTypeConstants.BidPrice
                    },
                    Value = snapshotEvent.Bids.First().Price
                });
            }

            if (snapshotEvent.Bids.Any() && snapshotEvent.Asks.Any())
            {
                marketDataValues.Add(new MarketDataValue
                {
                    MarketDataType = new MarketDataType
                    {
                        Code = MarketDataTypeConstants.BidAskSpread
                    },
                    Value = snapshotEvent.Bids.First().Price - snapshotEvent.Asks.First().Price
                });
            }

            return new MarketDataEntry
            {
                TimeStamp = DateTime.UtcNow,
                Product = new Data.context.Product
                {
                    Code = snapshotEvent.ProductId
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
