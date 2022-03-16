using Data.constants;
using Data.context;
using Data.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.helpers;

namespace DataProcessing.calculations
{
    public class BidAskSpreadCalculation : Calculation
    {
        protected override MarketDataEntry Calculate(MarketDataEntry currentMarketData, List<MarketDataEntry> previousMarketData)
        {
            var bidPrice = currentMarketData.MarketDataValues
                .SingleOrDefault(v => v.MarketDataType.Code == MarketDataTypeConstants.BidPrice)
                ?.Value;

            var askPrice = currentMarketData.MarketDataValues
                .SingleOrDefault(v => v.MarketDataType.Code == MarketDataTypeConstants.AskPrice)
                ?.Value;

            if (bidPrice != null && askPrice != null)
            {
                currentMarketData.MarketDataValues.Add(new MarketDataValue
                {
                    MarketDataType = new MarketDataType
                    {
                        Code = MarketDataTypeConstants.BidAskSpread
                    },
                    Value = (decimal) (bidPrice - askPrice)
                });
            }

            return currentMarketData;
        }
    }
}
