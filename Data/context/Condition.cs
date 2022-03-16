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
        public int LeftExpressionId { get; set; }
        public virtual Expression LeftExpression { get; set; }
        public int RightExpressionId { get; set; }
        public virtual Expression RightExpression { get; set; }
        public int ConditionTypeId { get; set; }
        public virtual ConditionType ConditionType { get; set; }
        public int WorkflowActionId { get; set; }
        public virtual WorkflowAction WorkflowAction { get; set; }
    }
}
