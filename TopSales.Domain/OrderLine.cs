namespace TopSales.Domain
{
    public class OrderLine
    {
        public string MerchantProductNo { get; set; }
        public string GTIN { get; set; }
        public int Quantity { get; set; }
    }
}