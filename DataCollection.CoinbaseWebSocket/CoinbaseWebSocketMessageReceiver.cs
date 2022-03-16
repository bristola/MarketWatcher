using Coinbase.Pro.Models;
using Coinbase.Pro.WebSockets;
using DataCollectionCoinbaseWebSocket.contracts;
using DataProcessing.contracts;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocket4Net;

namespace DataCollectionCoinbaseWebSocket
{
    public class CoinbaseMessageReceiver : ICoinbaseWebSocketMessageReceiver
    {
        private readonly IMessageProcessorFactory _messageProcessorFactory;
        public CoinbaseMessageReceiver(IMessageProcessorFactory messageProcessorFactory)
        {
            _messageProcessorFactory = messageProcessorFactory;
        }

        public void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (WebSocketHelper.TryParse(e.Message, out var parsedMessage))
            {
                var ev = parsedMessage as Event;
                if (ev != null)
                {
                    var processor = _messageProcessorFactory.Create(ev?.Type);
                    processor?.Process(ev);
                }
            }
        }
    }
}
