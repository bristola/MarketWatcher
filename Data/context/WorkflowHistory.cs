using System;
using System.Collections.Generic;
using System.Text;

namespace Data.context
{
    public class WorkflowHistory
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int WorkflowId { get; set; }
        public virtual Workflow Workflow { get; set; }
        public int WorkflowActionId { get; set; }
        public virtual WorkflowAction WorkflowAction { get; set; }

    }
}
