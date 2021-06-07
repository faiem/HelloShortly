using HelloShortly.UrlDistributerRestApi.ConcurrentQ;
using HelloShortly.UrlDistributerRestApi.Data.Entities;
using HelloShortly.UrlDistributerRestApi.Data.Repositories;
using HelloShortly.UrlDistributerRestApi.Dtos.Responses;
using HelloShortly.UrlDistributerRestApi.ShortAliasesHelper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HelloShortly.UrlDistributerRestApi.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly ILogger<ShortUrlService> _logger;
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly ConcurrentQueue _concurrentQueue;
        private readonly IBase62Generator _base62Generator;

        public ShortUrlService(ILogger<ShortUrlService> logger,
            IShortUrlRepository shortUrlRepository,
            ConcurrentQueue concurrentQueue,
            IBase62Generator base62Generator)
        {
            _logger = logger;
            _shortUrlRepository = shortUrlRepository;
            _concurrentQueue = concurrentQueue;
            _base62Generator = base62Generator;
        }

        public async Task<ShortUrlResponse> GenerateShortUrl(string uri, string customHostedName, CancellationToken ct = default)
        {
            string shortAliases = "";
            long unique_number = -1;
            if (_concurrentQueue.QSize() > 0)
            {
                unique_number = await _concurrentQueue.ReadAsync();
            }
            else
            {
                //no keys are available, very rare situation
                return null;
            }

            shortAliases = _base62Generator.Encode(unique_number);

            if (string.IsNullOrEmpty(customHostedName))
            {
                customHostedName = Environment.GetEnvironmentVariable("LOAD_BALANCER");
                //_logger.LogInformation($"customHostedName became {customHostedName}");
            }

            ShortUrl su = new()
            {
                CreatedAt = DateTime.UtcNow,
                //default expires after 3 years
                ExpirationAt = DateTime.UtcNow.AddYears(3),
                Id = shortAliases,
                OriginalUrl = uri,
                CustomHostedUrl = customHostedName + shortAliases,
                UserId = null
            };

            //_shortUrlRepository.Create(su);

            ShortUrlResponse response = new()
            {
                CreatedAt = su.CreatedAt,
                LongUrl = su.OriginalUrl,
                ShortUrl = customHostedName + shortAliases,
                ShortUrlAliases = shortAliases
            };

            return response;
        }

        public string GetLongUrl(string aliases)
        {
            var su = _shortUrlRepository.Find(aliases);
            if (su != null)
            {
                return su.OriginalUrl;
            }
            return null;
        }
    }
}
