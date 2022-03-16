using System;
using System.Collections.Generic;
using System.Text;

namespace Data.context
{
    public class MarketDataValue
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public int MarketDataTypeId { get; set; }
        public virtual MarketDataType MarketDataType { get; set; }
        public int MarketDataEntryId { get; set; }
        public virtual MarketDataEntry MarketDataEntry { get; set; }
    }
}
