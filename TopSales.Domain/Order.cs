using System.Collections.Generic;

namespace TopSales.Domain
{
    public class Order
    {
        public string Status { get; set; }
        public List<OrderLine> Lines { get; set; }
    }
}