namespace Data.data
{
    public class WorkflowActionDTO
    {
        public int WorkflowActionId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public int NextWorkFlowActionId { get; set; }
        public virtual WorkflowActionTypeDTO WorkflowActionType { get; set; }
    }
}