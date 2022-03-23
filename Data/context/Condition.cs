using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.context
{
    public class Condition
    {
        public int Id { get; set; }
        public virtual List<ConditionToken> Token { get; set; }
        public int ConditionTypeId { get; set; }
        public virtual ConditionType ConditionType { get; set; }
        public int WorkflowActionId { get; set; }
        public virtual WorkflowAction WorkflowAction { get; set; }
    }
}
