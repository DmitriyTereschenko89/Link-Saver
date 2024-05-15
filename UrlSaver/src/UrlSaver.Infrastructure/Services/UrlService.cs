using Microsoft.Extensions.Options;
using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Infrastructure.Services
{
    public class UrlService(IUrlRepository urlRepository, IEncodeService encodeService, IOptions<UrlLifespanOptions> options
        ) : IUrlService
    {
        private readonly IUrlRepository _urlRepository = urlRepository;
        private readonly IEncodeService _encodeService = encodeService;
        private readonly IOptions<UrlLifespanOptions> _options = options;
        public async Task<string> GetOriginalUrlAsync(string key)
        {
            return await _urlRepository.GetOriginalUrlAsync(key);
        }

        public async Task SaveUrlAsync(UrlModel originalUrl)
        {
            DateTimeOffset createdDate = DateTimeOffset.UtcNow;
            DateTimeOffset expiredDate = createdDate.AddDays(_options.Value.UrlLifespanInDays);
            originalUrl.CreatedDate = createdDate;
            originalUrl.ExpiredDate = expiredDate;
            originalUrl.ShortUrl = _encodeService.Encode(originalUrl.OriginalUrl);
            await _urlRepository.SaveUrlAsync(originalUrl);
        }
    }
}
