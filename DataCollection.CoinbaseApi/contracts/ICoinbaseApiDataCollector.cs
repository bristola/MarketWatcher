using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataCollection.CoinbaseApi.contracts
{
    public interface ICoinbaseApiDataCollector
    {
        Task Execute(CancellationToken cancellationToken);
    }
}
