using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.UrlDistributerRestApi.Dtos.Forms
{
    public class AddLongUrlForm
    {
        /// <summary>
        /// User provided long url
        /// </summary>
        [Required]
        public string LongUrl { get; set; }

        /// <summary>
        /// If user wants to serve the short url with custom hosting. It's 
        /// optional.
        /// </summary>
        [DefaultValue("http://localhost:5900/")]
        public string ShortUrlHost { get; set; }
    }
}
