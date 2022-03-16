using Coinbase.Pro.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.contracts
{
    public interface IMessageProcessor<T> where T : class
    {
        void Process(T data);
    }
}
