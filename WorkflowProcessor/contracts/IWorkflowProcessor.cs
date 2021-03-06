using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkflowProcessor.contracts
{
    public interface IWorkflowProcessor
    {
        Task Execute(CancellationToken cancellationToken);
    }
}
