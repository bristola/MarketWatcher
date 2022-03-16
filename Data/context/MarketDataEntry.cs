using System;
using System.Collections.Generic;
using System.Text;

namespace Data.context
{
    public class MarketDataEntry
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual List<MarketDataValue> MarketDataValues { get; set; }
    }
}
