using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowProcessor.contracts
{
    public interface IExpressionCalculatorFactory
    {
        IExpressionCalculator Create(string expressionTypeCode);
    }
}
