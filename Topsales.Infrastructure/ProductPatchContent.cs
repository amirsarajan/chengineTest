namespace Topsales.Infrastructure
{
    public class ProductPatchContent
    {
        public int RejectedCount { get; set; }
        public int AcceptedCount { get; set; }
        public ProductMessage[] ProductMessages { get; set; }     
    }
}
