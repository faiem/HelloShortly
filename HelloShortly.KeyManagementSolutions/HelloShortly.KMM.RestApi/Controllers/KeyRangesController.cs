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
        [HttpGet(Name = nameof(GetUniqueKeyRange))]
        public IActionResult GetUniqueKeyRange()
        {
            KeyRangeResponseDto response = _keyRangeProviderService.GetUniqueKeyRange();
            
            return Ok(response);
        }
    }
}
