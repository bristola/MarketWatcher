using Coinbase.Pro;
using Coinbase.Pro.Models;
using Data.constants;
using DataAccess.contracts;
using DataCollection.CoinbaseApi.contracts;
using DataProcessing.contracts;
using DataProcessing.processors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities.services.contracts;

namespace DataCollection.CoinbaseApi
{
    public class CoinbaseApiDataCollector : ICoinbaseApiDataCollector
    {
        private readonly IMarketDataQueries _marketDataQueries;
        private readonly IMarketDataCommands _marketDataCommands;
        private readonly IConfigurationService _configurationService;
        private readonly CoinbaseProClient _client;

        public CoinbaseApiDataCollector(IMarketDataQueries marketDataQueries, IMarketDataCommands marketDataCommands, IConfigurationService configurationService)
        {
            _marketDataQueries = marketDataQueries;
            _marketDataCommands = marketDataCommands;
            _configurationService = configurationService;
            _client = new CoinbaseProClient();
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            var products = _marketDataQueries.GetProducts(ProductTypeConstants.Crypto);
            foreach (var product in products)
            {
                var tickerData = await _client.MarketData.GetTickerAsync(product.Code);
                var processor = CreateProcessor(product.Code);
                processor.Process(tickerData);
            }

            var delaySeconds = _configurationService.GetInt(ConfigurationConstants.DataCollectionCoinbaseApiDelay);
            await Task.Delay(delaySeconds * 1000, cancellationToken);
        }

        private IMessageProcessor<Ticker> CreateProcessor(string productCode)
        {
            return new CoinbaseTickerProcessor(productCode, _marketDataQueries, _marketDataCommands);
        }
    }
}
