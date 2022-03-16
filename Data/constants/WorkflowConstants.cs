using System;
using System.Collections.Generic;
using System.Text;

namespace Data.constants
{
    public static class WorkflowConstants
    {
        public static class Actions
        {
            public const string Condition = "COND";
            public const string None = "NONE";
            public const string Email = "EMAIL";
            public const string Buy = "BUY";
            public const string Sell = "SELL";
        }

        public static class ConditionTypes
        {
            public const string GreaterThan = "GRT";
            public const string GreaterThanOrEqual = "GRTE";
            public const string LessThan = "LT";
            public const string LessThanOrEqual = "LTE";
            public const string Equal = "EQL";
        }

        public static class ExpressionTypes
        {
            public const string Addition = "ADD";
            public const string Subtraction = "SUB";
            public const string Division = "DIV";
            public const string Multiply = "MUL";
            public const string Value = "VAL";
            public const string Constant = "CON";
        }
    }
}
