namespace Topsales.Infrastructure
{
    public class ProductMessage
    {
        public string Name { get; set; }
        public string Reference { get; set; }
        public string[] Warnings { get; set; }
        public string[] Errors { get; set; }
    }
}
