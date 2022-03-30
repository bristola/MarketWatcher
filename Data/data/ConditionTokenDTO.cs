namespace Data.data
{
    public class ConditionTokenDTO
    {
        public int ConditionTokenId { get; set; }
        public decimal? ConstantValue { get; set; }
        public virtual ProductDTO? Product { get; set; }
        public ConditionTokenTypeDTO ConditionTokenType { get; set; }
        public MarketDataTypeDTO? MarketDataType { get; set; }
    }
}