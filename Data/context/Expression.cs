namespace Data.context
{
    public class Expression
    {
        public int Id { get; set; }
        public int ProductOneId { get; set; }
        public virtual Product ProductOne { get; set; }
        public int MarketDataTypeOneId { get; set; }
        public virtual MarketDataType MarketDataTypeOne { get; set; }
        public int ProductTwoId { get; set; }
        public virtual Product ProductTwo { get; set; }
        public int MarketDataTypeTwoId { get; set; }
        public virtual MarketDataType MarketDataTypeTwo { get; set; }
        public int ExpressionTypeId { get; set; }
        public virtual ExpressionType ExpressionType { get; set; }
    }
}