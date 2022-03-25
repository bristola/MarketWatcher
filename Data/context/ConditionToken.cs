namespace Data.context
{
    public class ConditionToken
    {
        public int Id { get; set; }
        public bool IsLeftExpression { get; set; }
        public int ConditionTokenTypeId { get; set; }
        public virtual ConditionTokenType ConditionTokenType { get; set; }
        public decimal? ConstantValue { get; set; }
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int? MarketDataTypeId { get; set; }
        public virtual MarketDataType? MarketDataType { get; set; }
        public int ConditionId { get; set; }
        public virtual Condition Condition { get; set; }
    }
}