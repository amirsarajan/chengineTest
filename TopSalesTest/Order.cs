using System.Collections.Generic;

namespace TopSalesTest
{
    public class Order
    {
        public string Status { get; set; }
        public List<OrderLine> Lines { get; set; }
    }
}