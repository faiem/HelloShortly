using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.UrlDistributerRestApi.Dtos.Responses
{
    public class ShortUrlResponse
    {
        public string ShortUrlAliases { get; set; }

        public string ShortUrl { get; set; }

        public string LongUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
