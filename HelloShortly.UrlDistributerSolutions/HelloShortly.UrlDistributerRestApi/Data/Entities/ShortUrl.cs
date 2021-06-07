using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.UrlDistributerRestApi.Data.Entities
{
    public class ShortUrl
    {
        [BsonId]
        public string Id { get; set; }

        //Long Url
        public string OriginalUrl { get; set; }

        //OPtional Feature
        public string CustomHostedUrl { get; set; }

        //For future support
        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpirationAt { get; set; }
    }
}
