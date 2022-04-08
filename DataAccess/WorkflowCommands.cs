using Data.context;
using Data.data;
using DataAccess.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class WorkflowCommands : IWorkflowCommands
    {
        private readonly MarketContext _context;

        public WorkflowCommands(MarketContext context)
        {
            _context = context;
        }

        public void SaveWorkflow(WorkflowDTO workflow)
        {
            var newWorkflow = new Workflow();
        }
    }
}
