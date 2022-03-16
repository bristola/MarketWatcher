using Data.context;
using DataAccess.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowProcessor.contracts;

namespace WorkflowProcessor.validators
{
    public class GreaterThanValidator : IConditionValidator
    {
        public bool Validate(decimal leftValue, decimal rightValue) => leftValue > rightValue;
    }
}
