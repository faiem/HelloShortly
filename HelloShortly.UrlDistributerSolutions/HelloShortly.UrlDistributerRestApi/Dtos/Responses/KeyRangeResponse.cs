using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.UrlDistributerRestApi.Dtos.Responses
{
    public class KeyRangeResponse
    {
        public int range_id { get; set; }

        public long start_range { get; set; }

        public long end_range { get; set; }
    }
}
