using BlazorWebUI.Bussines;
using BlazorWebUI.Databases.Mongo.DataModels;

namespace BlazorWebUI.Models
{
    public class FollowedProductDto
    {
        public List<SubItem> Items { get; set; }
        public class SubItem
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public long ProductId { get; set; }
            public string Barcode { get; set; }
            public int Stock { get; set; }
            public string ModelNo { get; set; }
            public decimal Price { get; set; }
            public decimal TrendyolPrice{get;set; }
            public int TrendyolStock { get; set; }
            public string TrendyolLink { get; set; }
            public string Link { get; set; }
            public string Description { get; set; }
            public List<string> ImageLinks { get; set; }
            public List<string> Categories { get; set; }
            public List<string> ProductTypes { get; set; }
            public SourceSite SourceSite { get; set; }
            public Item TahtaKaleItem { get; set; }
            public  FollowedProduct FollowedProduct { get; set; }
        }
    }
    public class FollowedProductAddDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public SourceSite SourceSite { get; set; }
    }
    public class FollowedProductUpdate
    {
        public string Id { get; set; }
        public decimal TrendyolPrice { get; set; }
        public int TrendyolStock { get; set; }
        public string TrendyolLink { get; set; }
    }
}
