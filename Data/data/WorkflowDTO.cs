using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.data
{
    public class WorkflowDTO
    {
        public int WorkflowId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CurrentIteration { get; set; } = 0;
        public int Iterations { get; set; }
        public WorkflowStatusDTO WorkflowStatus { get; set; }
        public WorkflowActionDTO CurrentWorkflowAction { get; set; }
        public List<WorkflowActionDTO> WorkflowActions { get; set; }
    }
}
