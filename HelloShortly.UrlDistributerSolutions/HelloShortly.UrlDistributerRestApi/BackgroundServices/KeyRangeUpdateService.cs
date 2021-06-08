using HelloShortly.UrlDistributerRestApi.ConcurrentQ;
using HelloShortly.UrlDistributerRestApi.Dtos.Responses;
using HelloShortly.UrlDistributerRestApi.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HelloShortly.UrlDistributerRestApi.BackgroundServices
{
    public class KeyRangeUpdateService : BackgroundService
    {
        private readonly ILogger<KeyRangeUpdateService> _logger;
        private readonly ConcurrentQueue _concurrentQueue;
        private IHttpClientFactory _clientFactory;

        public KeyRangeUpdateService(ILogger<KeyRangeUpdateService> logger,
            ConcurrentQueue concurrentQueue, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _concurrentQueue = concurrentQueue;
            _clientFactory = clientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Background Task has started, UTC Time: {DateTime.UtcNow}"); ;
            
            while (!stoppingToken.IsCancellationRequested)
            {

                if (_concurrentQueue.QSize() <= 0)
                {
                    try
                    {
                        var request = new HttpRequestMessage(HttpMethod.Get,
                        Environment.GetEnvironmentVariable("KEY_RANGE_GET_API"));

                        var client = _clientFactory.CreateClient();

                        var response = await client.SendAsync(request);

                        if (response.IsSuccessStatusCode)
                        {
                            using var responseStream = await response.Content.ReadAsStreamAsync();

                            var res = await JsonSerializer.DeserializeAsync<KeyRangeResponse>(responseStream);
                            _logger.LogInformation($"Key Ranges are {res.start_range} {res.end_range}");
                           
                            for (long I = res.start_range; I <= res.end_range; I++)
                            {
                                _concurrentQueue.Write(I);
                            }
                        }
                        else
                        {
                            _logger.LogError("Failed to retrieve key ranges from keyRange Service.");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("Failed to get keygen range. -> " + ex.Message);
                    }
                }

                //Check the Q in every 5 seconds
                await Task.Delay(5000, stoppingToken);
            }


            _logger.LogInformation($"Background Task has stopped, UTC Time: {DateTime.UtcNow}"); ;

        }
    }
}
