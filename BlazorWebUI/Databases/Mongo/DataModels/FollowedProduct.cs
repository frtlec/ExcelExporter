using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace BlazorWebUI.Databases.Mongo.DataModels
{
    public class FollowedProduct
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public decimal TrendyolPrice { get; set; }
        public int TrendyolStock { get; set; }
        public string TrendyolLink { get; set; }

        [BsonRepresentation(BsonType.String)]
        public SourceSite SourceSite { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public enum SourceSite
    {
        TAHTAKALE
    }
}
