using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebScraping.Models
{
    public class ScrapingResult
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        [JsonProperty("Name")]
        public string  SiteName { get; set; }
        public string Title { get; set; }
        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }
        public string ScreenCapture { get; set; }
        public List<string> Hyperlinks { get; set; }
        public List<string> SocialMediaLinks { get; set; }

    }

}
