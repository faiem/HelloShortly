using HelloShortly.UrlDistributerRestApi.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HelloShortly.UrlDistributerRestApi.Services
{
    public interface IShortUrlService
    {
        Task<ShortUrlResponse> GenerateShortUrl(string uri, string customHostName, CancellationToken ct = default);
        string GetLongUrl(string aliases);
    }
}
