using Data.context;
using Data.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowProcessor.contracts
{
    public interface IExpressionCalculator
    {
        decimal Calculate(List<ConditionTokenDTO> tokens);
    }
}
