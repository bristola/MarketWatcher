using System;
using System.Collections.Generic;

namespace Data.context
{
    public class Workflow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CurrentWorkflowActionId { get; set; }
        public WorkflowAction CurrentWorkflowAction { get; set; }
        public List<WorkflowAction> WorkflowActions { get; set; }
    }
}