using HelloShortly.KMM.RestApi.Dtos.Responses;
using HelloShortly.KMM.RestApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.KMM.RestApi.Controllers
{
    [Route("/[Controller]")]
    [ApiController]
    public class KeyRangesController : Controller
    {
        private IKeyRangeProviderService _keyRangeProviderService;
        public KeyRangesController(IKeyRangeProviderService keyRangeProvider)
        {
            _keyRangeProviderService = keyRangeProvider;
        }

        /// <summary>
        /// It provides unique Key Ranges to the consumer.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(KeyRangeResponseDto), 200)]
        [HttpGet("GetUniqueKeyRange")]
        public IActionResult GetUniqueKeyRange()
        {
            KeyRangeResponseDto response = _keyRangeProviderService.GetUniqueKeyRange();

            if (response.range_id == -1)
            {
                //Send notification to admin to inform that all of the key ranges are finished.
            }

            return Ok(response);
        }
    }
}
