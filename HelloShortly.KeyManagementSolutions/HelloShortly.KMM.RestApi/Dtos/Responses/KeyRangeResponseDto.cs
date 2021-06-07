using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.KMM.RestApi.Dtos.Responses
{
    public class KeyRangeResponseDto
    {
        public KeyRangeResponseDto(int id, long start, long end)
        {
            range_id = id;
            start_range = start;
            end_range = end;
        }

        public int range_id { get; set; }

        public long start_range { get; set; }

        public long end_range { get; set; }
    }
}
