using Data.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowProcessor.contracts
{
    public interface IConditionValidator
    {
        bool Validate(decimal leftValue, decimal rightValue);
    }
}
