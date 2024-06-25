using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using UrlSaver.Api.DataTransferObjects;
using UrlSaver.Api.Exceptions;
using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Api.Controllers
{
    [ApiController]
    public class UrlController(IUrlService urlService, IMapper mapper, IMemoryCache memoryCache, IOptions<CacheEntryOptions> cacheEntryOptions) : ControllerBase
    {
        private readonly IUrlService _urlService = urlService;
        private readonly IMapper _mapper = mapper;
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly IOptions<CacheEntryOptions> _cacheEntryOptions = cacheEntryOptions;

        [HttpGet]
        [Route("/{key}")]
        public async Task<UrlDto> Get(string key)
        {
            if (!_memoryCache.TryGetValue(key, out string originalUrl))
            {
                originalUrl = await _urlService.GetOriginalUrlAsync(key);

                if (string.IsNullOrEmpty(originalUrl))
                {
                    throw new ItemNotFoundException();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSize(_cacheEntryOptions.Value.SizeLimit)
                    .SetSlidingExpiration(TimeSpan.FromSeconds(_cacheEntryOptions.Value.SlidingExpirationSeconds))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(_cacheEntryOptions.Value.AbsoluteExpirationSeconds));

                _ = _memoryCache.Set(key, originalUrl, cacheEntryOptions);
            }

            return _mapper.Map<UrlDto>(originalUrl);
        }

        [HttpPost]
        [Route("/")]
        public async Task<UrlDto> CreateShortUrl([FromBody] UrlDto originalUrlDto)
        {
            var originalUrl = _mapper.Map<UrlModel>(originalUrlDto);
            await _urlService.SaveUrlAsync(originalUrl);

            return _mapper.Map<UrlDto>(originalUrl);
        }
    }
}
