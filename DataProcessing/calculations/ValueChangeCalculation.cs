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
    public class ValueChangeCalculation : Calculation
    {
        private readonly string _inputMarketType;
        private readonly string _outputMarketType;
        private readonly int _minutes;

        public ValueChangeCalculation(string inputMarketType, string outputMarketType, int minutes)
        {
            _inputMarketType = inputMarketType;
            _outputMarketType = outputMarketType;
            _minutes = minutes;
        }

        protected override MarketDataEntry Calculate(MarketDataEntry currentMarketData, List<MarketDataEntry> previousMarketData)
        {
            var data = new List<MarketDataEntry>(previousMarketData);
            data.Add(currentMarketData);

            var minutesAgo = DateTime.UtcNow.AddMinutes(-1 * _minutes);
            var filteredData = data.Where(d => d.TimeStamp > minutesAgo)
                .OrderBy(d => d.TimeStamp)
                .SelectMany(d => d.MarketDataValues, (Entry, MarketValue) => new { Entry.TimeStamp, MarketValue })
                .Where(d => d.MarketValue.MarketDataType.Code == _inputMarketType)
                .ToList();

            var minTime = filteredData.Select(c => c.TimeStamp).Min();

            var coordinates = filteredData
                .Select(d => new Coordinate {
                    X = d.TimeStamp.Ticks - minTime.Ticks,
                    Y = d.MarketValue.Value
                })
                .ToList();

            // Only calculate values change if more than 1 entry exists
            if (coordinates.Count > 1)
            {
                var mathHelper = new MathematicsHelper();
                var slope = mathHelper.LeastSquaresSlope(coordinates);

                currentMarketData.MarketDataValues.Add(new MarketDataValue
                {
                    Value = slope,
                    MarketDataType = new MarketDataType
                    {
                        Code = _outputMarketType
                    }
                });
            }


            return currentMarketData;
        }
    }
}
