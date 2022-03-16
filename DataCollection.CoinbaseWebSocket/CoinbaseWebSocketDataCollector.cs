using Coinbase.Pro.Models;
using Coinbase.Pro.WebSockets;
using Data.constants;
using DataCollectionCoinbaseWebSocket.contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities.services.contracts;

namespace DataCollectionCoinbaseWebSocket
{
    public class CoinbaseDataCollector : ICoinbaseWebSocketDataCollector
    {
        private readonly IConfigurationService _configService;
        private readonly ICoinbaseWebSocketMessageReceiver _messageReceiver;

        public CoinbaseDataCollector(IConfigurationService configService, ICoinbaseWebSocketMessageReceiver messageReceiver)
        {
            _configService = configService;
            _messageReceiver = messageReceiver;
        }

        public async Task Start(CancellationToken token)
        {
            using (var webSocket = new CoinbaseProWebSocket())
            {
                var connectionResult = await webSocket.ConnectAsync();

                if (!connectionResult.Success)
                {
                    throw new Exception("An errored occured while trying to connect to the coinbase websocket");
                }

                var subscription = new Subscription
                {
                    ProductIds =
                    {
                        "BTC-USD"
                    },
                    Channels =
                    {
                        "ticker"
                    }
                };

                await webSocket.SubscribeAsync(subscription);

                webSocket.RawSocket.MessageReceived += _messageReceiver.MessageReceived;

                await Task.Delay(TimeSpan.FromMinutes(_configService.GetDouble(ConfigurationConstants.WebSocketResetDelay)));
            }

        }
    }
}
