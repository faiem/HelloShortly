using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HelloShortly.UrlDistributerRestApi.ConcurrentQ
{
    public class ConcurrentQueue
    {
        private ConcurrentQueue<long> _queue = new();
        private SemaphoreSlim _semaphore = new(0);

        public int QSize()
        {
            return _queue.Count;
        }

        public void Write(long value)
        {
            _queue.Enqueue(value);
            _semaphore.Release();
        }

        public async ValueTask<long> ReadAsync(CancellationToken cancellationToken = default)
        {
            await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
            _queue.TryDequeue(out var item);
            return item;
        }
    }
}
