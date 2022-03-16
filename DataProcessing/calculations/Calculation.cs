using Coinbase.Pro.Models;
using Data.context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.calculations
{
    public abstract class Calculation
    {
        private Calculation nextCalculation;

        public Calculation SetNext(Calculation marketCalculation)
        {
            nextCalculation = marketCalculation;
            return marketCalculation;
        }

        public MarketDataEntry Execute(MarketDataEntry currentMarketData, List<MarketDataEntry> previousMarketData)
        {
            currentMarketData = Calculate(currentMarketData, previousMarketData);
            if (nextCalculation != null)
            {
                return nextCalculation.Execute(currentMarketData, previousMarketData);
            }
            return currentMarketData;
        }

        protected abstract MarketDataEntry Calculate(MarketDataEntry currentMarketData, List<MarketDataEntry> previousMarketData);
    }
}
