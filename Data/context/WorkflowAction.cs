using System;

namespace Data.context
{
    public class WorkflowAction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public int? NextWorkFlowActionId { get; set; }
        public virtual WorkflowAction NextWorkFlowAction { get; set; }
        public int WorkflowId { get; set; }
        public virtual Workflow Workflow { get; set; }
        public int WorkflowActionTypeId { get; set; }
        public virtual WorkflowActionType WorkflowActionType { get; set; }
    }
}