using Coinbase.Pro.Models;
using Data.constants;
using Data.context;
using DataAccess.contracts;
using DataProcessing.calculations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.processors
{
    public class CoinbaseTickerProcessor : BaseProcessor<Ticker>
    {
        private readonly string _productCode;

        public CoinbaseTickerProcessor(string productCode, IMarketDataQueries marketDataQueries, IMarketDataCommands marketDataCommands) : base(marketDataQueries, marketDataCommands)
        {
            _productCode = productCode;
        }

        protected override MarketDataEntry CreateMarketDataEntry(Ticker data)
        {
            var marketDataValues = new List<MarketDataValue>()
            {
                new MarketDataValue
                {
                    MarketDataType = new MarketDataType
                    {
                        Code = MarketDataTypeConstants.AskPrice
                    },
                    Value = data.Ask
                },
                new MarketDataValue
                {
                    MarketDataType = new MarketDataType
                    {
                        Code = MarketDataTypeConstants.BidPrice
                    },
                    Value = data.Bid
                },
                new MarketDataValue
                {
                    MarketDataType = new MarketDataType
                    {
                        Code = MarketDataTypeConstants.BidAskSpread
                    },
                    Value = data.Bid - data.Ask
                }
            };

            return new MarketDataEntry
            {
                TimeStamp = DateTime.UtcNow,
                Product = new Data.context.Product
                {
                    Code = _productCode
                },
                MarketDataValues = marketDataValues
            };
        }

        protected override MarketDataEntry Calculate(MarketDataEntry currentMarketData, List<MarketDataEntry> previousMarketData)
        {
            var calculation = new BidAskSpreadCalculation();
            calculation.SetNext(new ValueChangeCalculation(MarketDataTypeConstants.BidPrice, MarketDataTypeConstants.FiveMinutePriceChange, 15))
                .SetNext(new ValueChangeCalculation(MarketDataTypeConstants.BidPrice, MarketDataTypeConstants.FifteenMinutePriceChange, 15));

            return calculation.Execute(currentMarketData, previousMarketData);
        }
    }
}
