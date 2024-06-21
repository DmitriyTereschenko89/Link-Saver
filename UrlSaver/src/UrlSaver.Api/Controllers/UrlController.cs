using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UrlSaver.Api.DataTransferObjects;
using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Api.Controllers
{
    [ApiController]
    public class UrlController(IUrlService urlService, IMapper mapper, ICacheService cacheService) : ControllerBase
    {
        private readonly IUrlService _urlService = urlService;
        private readonly IMapper _mapper = mapper;
        private readonly ICacheService _cacheService = cacheService;

        [HttpGet]
        [Route("/{key}")]
        public async Task<UrlDto> Get(string key)
        {
            string originalUrl = await _cacheService.GetOrCreateAsync(key, async () => await _urlService.GetOriginalUrlAsync(key));

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
