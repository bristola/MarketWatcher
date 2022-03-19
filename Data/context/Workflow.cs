using System;
using System.Collections.Generic;

namespace Data.context
{
    public class Workflow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CurrentIteration { get; set; } = 0;
        public int Iterations { get; set; }
        public int CurrentWorkflowActionId { get; set; }
        public int WorkflowStatusId { get; set; }
        public virtual WorkflowStatus WorkflowStatus { get; set; }
        public WorkflowAction CurrentWorkflowAction { get; set; }
        public List<WorkflowAction> WorkflowActions { get; set; }
    }
}