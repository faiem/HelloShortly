using HelloShortly.KMM.RestApi.Data.Repositories;
using HelloShortly.KMM.RestApi.Dtos.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.KMM.RestApi.Services
{
    public class KeyRangeProviderService : IKeyRangeProviderService
    {
        private ILogger<KeyRangeProviderService> _logger;
        private readonly IKeyRangeRepository _keyRangeRepository;
        public KeyRangeProviderService(ILogger<KeyRangeProviderService> logger, IKeyRangeRepository keyRangeRepository)
        {
            _logger = logger;
            _keyRangeRepository = keyRangeRepository;
        }
        public KeyRangeResponseDto GetUniqueKeyRange()
        {
            string retrieveKeyIdentifier = Guid.NewGuid().ToString();
            int rowUpdated = _keyRangeRepository.UpdateKeyRangeWithUniqueIdentifier(retrieveKeyIdentifier);
            
            var key_range_Response = new KeyRangeResponseDto(-1, 0, 0);
            var keyRange = _keyRangeRepository.GetUniqueKeyRangeByIdentifier(retrieveKeyIdentifier);
            
            if (keyRange != null)
            {
                key_range_Response.range_id = keyRange.key_ranges_id;
                key_range_Response.start_range = keyRange.start_range;
                key_range_Response.end_range = keyRange.end_range;
            }

            return key_range_Response;
        }

    }
}
