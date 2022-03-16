using Coinbase.Pro.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.contracts
{
    public interface IMessageProcessorFactory
    {
        IMessageProcessor<Event> Create(string type);
    }
}
