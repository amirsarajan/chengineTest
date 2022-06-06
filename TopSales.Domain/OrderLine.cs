namespace TopSales.Domain
{
    public class OrderLine
    {
        public string MerchantProductNo { get; set; }
        public string Gtin { get; set; }
        public int Quantity { get; set; }
    }
}