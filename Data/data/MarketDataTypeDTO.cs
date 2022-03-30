namespace Data.data
{
    public class MarketDataTypeDTO
    {
        public int MarketDataTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int ExpirationMinutes { get; set; }
    }
}