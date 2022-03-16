using Data.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowProcessor.contracts
{
    public interface IConditionValidatorFactory
    {
        IConditionValidator Create(string conditionTypeCode);
    }
}
