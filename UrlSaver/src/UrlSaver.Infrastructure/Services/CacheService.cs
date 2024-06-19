using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using UrlSaver.Domain.Common;

namespace UrlSaver.Infrastructure.Services
{
    public class CacheService (IMemoryCache cache) : ICacheService
    {
        private readonly IMemoryCache _cache = cache;
        private readonly ConcurrentDictionary<object, SemaphoreSlim> _lock = new();

        public async Task<string> GetOrCreateAsync(string key, Func<Task<string>> createKey)
        {
            if (!_cache.TryGetValue(key, out string originalUrl))
            {
                SemaphoreSlim semaphoreLocks = _lock.GetOrAdd(key, new SemaphoreSlim(1, 1));

                await semaphoreLocks.WaitAsync();
                try
                {
                    if (!_cache.TryGetValue(key, out originalUrl))
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSize(1)
                        .SetSlidingExpiration(TimeSpan.FromSeconds(10))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

                        originalUrl = await createKey();

                        if (!string.IsNullOrEmpty(originalUrl))
                        {
                            _cache.Set(key, originalUrl, cacheEntryOptions);
                        }
                    }
                }
                finally
                {
                    semaphoreLocks.Release();
                }
            }

            return originalUrl;
        }
    }
}
