namespace bookshop.webapi.Models
{
    public class GoogleApiResponse
    {
        public List<GoogleBook> Items { get; set; }
    }

    public class GoogleBook
    {
        public VolumeInfo VolumeInfo { get; set; }
        public SaleInfo SaleInfo { get; set; }
    }
    public class SaleInfo
    {
        public RetailPrice RetailPrice { get; set; }
        public string Saleability { get; set; }
    }
    public class RetailPrice 
    {
        public double Amount { get; set; }
        public string CurrencyCode { get; set; }
    }
    public class VolumeInfo
    {
        public ImageLinks ImageLinks { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public List<string> Authors { get; set; }  
        public List<IndustryIdentifiers> IndustryIdentifiers { get; set; }
    }

    public class IndustryIdentifiers
    {
        public string Type { get; set; }
        public string Identifier { get; set; }
    }

    public class ImageLinks
    {
        public string Thumbnail { get; set; }
    }

}
