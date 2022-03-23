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
            public const string Timer = "TIMER";
        }

        public static class ConditionTypes
        {
            public const string GreaterThan = "GRT";
            public const string GreaterThanOrEqual = "GRTE";
            public const string LessThan = "LT";
            public const string LessThanOrEqual = "LTE";
            public const string Equal = "EQL";
        }

        public static class ConditionTokenTypes
        {
            public const string Addition = "ADD";
            public const string Subtraction = "SUB";
            public const string Multiplication = "MULT";
            public const string Division = "DIV";
            public const string Constant = "CON";
            public const string MarketValue = "MVAL";
            public const string OpenParenthesis = "OPAR";
            public const string CloseParenthesis = "CPAR";
        }
    }
}
