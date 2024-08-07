﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Infrastructure.Services
{
    public class UrlService(IUrlRepository urlRepository,
                            IEncodeService encodeService,
                            IOptions<UrlLifespanOptions> options,
                            IMemoryCache memoryCache,
                            IOptions<CacheOptions> cacheEntryOptions) : IUrlService
    {
        private readonly IUrlRepository _urlRepository = urlRepository;
        private readonly IEncodeService _encodeService = encodeService;
        private readonly UrlLifespanOptions _options = options.Value;
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly CacheOptions _cacheEntryOptions = cacheEntryOptions.Value;

        public async Task<string> GetOriginalUrlAsync(string key)
        {
            if (!_memoryCache.TryGetValue(key, out string originalUrl))
            {
                originalUrl = await _urlRepository.GetOriginalUrlAsync(key);

                if (string.IsNullOrEmpty(originalUrl))
                {
                    return string.Empty;
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSize(_cacheEntryOptions.Size)
                    .SetSlidingExpiration(TimeSpan.FromSeconds(_cacheEntryOptions.SlidingExpirationSeconds))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(_cacheEntryOptions.AbsoluteExpirationSeconds));

                _ = _memoryCache.Set(key, originalUrl, cacheEntryOptions);
            }

            return originalUrl;
        }

        public async Task SaveUrlAsync(UrlModel originalUrl)
        {
            DateTimeOffset createdDate = DateTimeOffset.UtcNow;
            DateTimeOffset expiredDate = createdDate.AddDays(_options.UrlLifespanInDays);
            originalUrl.CreatedDate = createdDate;
            originalUrl.ExpiredDate = expiredDate;
            originalUrl.ShortUrl = _encodeService.Encode(originalUrl.OriginalUrl);
            await _urlRepository.SaveUrlAsync(originalUrl);
        }
    }
}
