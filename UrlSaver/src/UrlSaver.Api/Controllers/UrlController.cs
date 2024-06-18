using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

using UrlSaver.Api.DataTransferObjects;
using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Api.Controllers
{
    [ApiController]
    public class UrlController(IUrlService urlService, IMapper mapper, IMemoryCache cache, IOptions<UrlLifespanOptions> options) : ControllerBase
    {
        private readonly IOptions<UrlLifespanOptions> _options = options;
        private readonly IUrlService _urlService = urlService;
        private readonly IMapper _mapper = mapper;
        private readonly IMemoryCache _cache = cache;

        [HttpGet]
        [Route("/{key}")]
        public async Task<UrlDto> Get(string key)
        {
            TimeSpan _cacheExpiration = TimeSpan.FromDays(_options.Value.UrlLifespanInDays);
            if (!_cache.TryGetValue(key, out string originalUrl))
            {
                originalUrl = await _urlService.GetOriginalUrlAsync(key);
                if (!string.IsNullOrEmpty(originalUrl))
                {
                    _cache.Set(key, originalUrl, _cacheExpiration);
                }
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
