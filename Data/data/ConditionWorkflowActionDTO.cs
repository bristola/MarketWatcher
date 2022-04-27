using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.data
{
    public class ConditionWorkflowActionDTO
    {
        public int WorkflowId { get; set; }
        public WorkflowActionDTO WorkflowAction { get; set; }
        public ConditionDTO Condition { get; set; }
    }
}
