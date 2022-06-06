using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopSales.Domain
{
    public class Sale
    {
        public string MerchantProductNo { get; set; }
        public string ProductName { get; set; }
        public string GTIN { get; set; }
        public int SoldQuantity { get; set; }

    }
}
