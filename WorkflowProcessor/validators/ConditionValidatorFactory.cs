using Data.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowProcessor.contracts;
using Data.constants;

namespace WorkflowProcessor.validators
{
    public class ConditionValidatorFactory : IConditionValidatorFactory
    {
        public IConditionValidator Create(string conditionTypeCode)
        {
            switch (conditionTypeCode)
            {
                case WorkflowConstants.ConditionTypes.GreaterThan:
                    return new GreaterThanValidator();

                case WorkflowConstants.ConditionTypes.GreaterThanOrEqual:
                    return new GreaterThanOrEqualValidator();

                case WorkflowConstants.ConditionTypes.LessThan:
                    return new LessThanValidator();

                case WorkflowConstants.ConditionTypes.LessThanOrEqual:
                    return new LessThanOrEqualValidator();

                case WorkflowConstants.ConditionTypes.Equal:
                    return new EqualValidator();

                default:
                    throw new Exception($"Invalid condition type: {conditionTypeCode}");

            }
        }
    }
}
