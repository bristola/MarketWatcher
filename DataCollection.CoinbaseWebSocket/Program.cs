using Data.constants;
using Data.context;
using DataAccess;
using DataAccess.contracts;
using DataCollectionCoinbaseWebSocket.contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nito.AsyncEx;
using System;
using System.Threading;
using System.Threading.Tasks;
using Utilities.services;
using Utilities.services.contracts;
using DataProcessing.contracts;
using DataProcessing.processors;
using DataProcessing.helpers;

namespace DataCollectionCoinbaseWebSocket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configurationService = new ConfigurationService();
            var serviceProvider = new ServiceCollection()
                .AddScoped<ICoinbaseWebSocketMessageReceiver, CoinbaseMessageReceiver>()
                .AddScoped<IMessageProcessorFactory, MessageProcessorFactory>()
                .AddScoped<ICoinbaseWebSocketDataCollector, CoinbaseDataCollector>()
                .AddScoped<IMarketDataCommands, MarketDataCommands>()
                .AddScoped<IMarketDataQueries, MarketDataQueries>()
                .AddSingleton<IConfigurationService, ConfigurationService>()
                .AddDbContext<MarketContext>(options => options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(configurationService.GetConnectionString(ConfigurationConstants.DatabaseConnection)))
                .BuildServiceProvider();

            AsyncContext.Run(() => Execute(serviceProvider));
        }

        private static async Task Execute(ServiceProvider serviceProvider)
        {
            var token = new CancellationToken();
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var dataCollector = serviceProvider.GetService<ICoinbaseWebSocketDataCollector>();
                    await dataCollector.Start(token);
                }
                catch (Exception e)
                {
                    // We should probably log that there was an error, but continue
                }
            }
        }
    }
}
