using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataCollectionCoinbaseWebSocket.contracts
{
    public interface ICoinbaseWebSocketDataCollector
    {
        Task Start(CancellationToken token);
    }
}
