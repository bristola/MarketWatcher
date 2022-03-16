using System;
using System.Collections.Generic;
using System.Text;

namespace Data.context
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
