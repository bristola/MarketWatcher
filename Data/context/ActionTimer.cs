namespace Data.context
{
    public class ActionTimer
    {
        public int Id { get; set; }
        public int Seconds { get; set; }
        public DateTime? CurrentStartTime { get; set; }
        public DateTime? CurrentEndTime { get; set; }
        public int WorkflowActionId { get; set; }
        public WorkflowAction WorkflowAction { get; set; }
    }
}