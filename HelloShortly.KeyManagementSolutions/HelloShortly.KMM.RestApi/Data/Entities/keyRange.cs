using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.KMM.RestApi.Data.Entities
{
    public class keyRange
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int key_ranges_id { get; set; }

        public int start_range { get; set; }

        public int end_range { get; set; }

        /// <summary>
        /// Default value : false
        /// SET to true while updating for retrieval.
        /// </summary>
        public bool is_retrieved { get; set; }

        /// <summary>
        /// Default value : null
        /// SET to unique identifier while updating for retrieval.
        /// </summary>
        public string retrieve_key { get; set; }

        /// <summary>
        /// Default value : null
        /// SET to current UTC time while updating for retrieval.
        /// </summary>
        public DateTime? retrieved_time { get; set; }
    }
}
