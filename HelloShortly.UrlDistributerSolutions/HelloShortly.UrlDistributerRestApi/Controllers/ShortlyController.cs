using HelloShortly.UrlDistributerRestApi.Dtos.Forms;
using HelloShortly.UrlDistributerRestApi.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HelloShortly.UrlDistributerRestApi.Controllers
{
    [Route("/[Controller]")]
    [ApiController]
    public class ShortlyController : Controller
    {
        private readonly ILogger<ShortlyController> _logger;
        private readonly IShortUrlService _shortUrlService;
        private readonly ICacheService _cacheService;

        public ShortlyController(ILogger<ShortlyController> logger,
            IShortUrlService shortUrlService,
            ICacheService cacheService)
        {
            _logger = logger;
            _shortUrlService = shortUrlService;
            _cacheService = cacheService;
        }


        /// <summary>
        /// Generates short url from long url
        /// </summary>
        /// <param name="addLongUrlForm"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(AddLongUrl))]
        [SwaggerResponse(201, "Generate and Return short url.")]
        [SwaggerResponse(400, "Bad Request for invalid long url.")]
        public async Task<IActionResult> AddLongUrl([FromBody] AddLongUrlForm addLongUrlForm, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            //check if the url is well formed or not 
            if (!Uri.IsWellFormedUriString(addLongUrlForm.LongUrl, UriKind.Absolute))
            {
                return BadRequest(new
                {
                    Message = "Invalid Url."
                });
            }

            //encoded the url for storing
            string uri = UriHelper.Encode(new Uri(addLongUrlForm.LongUrl));

            var shortUrlResponse = await _shortUrlService.GenerateShortUrl(uri, addLongUrlForm.CustomShortUrlHost, ct);

            //Add to cache
            await _cacheService.SetCacheValueAsync(shortUrlResponse.ShortUrlAliases, shortUrlResponse.LongUrl);
            return StatusCode(201);
        }
    }
}
