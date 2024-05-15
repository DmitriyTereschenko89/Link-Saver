using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UrlSaver.Api.DataTransferObjects;
using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Api.Controllers
{
    [ApiController]
    public class UrlController(IUrlService urlService, IMapper mapper) : ControllerBase
    {
        private readonly IUrlService _urlService = urlService;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [Route("/{key}")]
        public async Task<IActionResult> Get(string key)
        {
            var originalUrl = await _urlService.GetOriginalUrlAsync(key);

            return string.IsNullOrEmpty(originalUrl) ? NotFound() : Redirect(originalUrl);
        }

        [HttpPost]
        [Route("/")]
        public async Task<IResult> CreateShortUrl([FromBody] UrlDto originalUrlDto)
        {
            var originalUrl = _mapper.Map<UrlModel>(originalUrlDto);
            await _urlService.SaveUrlAsync(originalUrl);

            return Results.Ok(originalUrl.ShortUrl);
        }
    }
}
