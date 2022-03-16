using System;
using System.Collections.Generic;
using System.Text;

namespace DataCollectionCoinbaseWebSocket.contracts
{
    public interface ICoinbaseWebSocketMessageReceiver
    {
        void MessageReceived(object sender, WebSocket4Net.MessageReceivedEventArgs e);
    }
}
