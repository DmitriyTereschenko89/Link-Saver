using Microsoft.Extensions.Logging;

using UrlSaver.Domain.Common;

namespace UrlSaver.Infrastructure.Services
{
    public class EncodeService(IUrlGeneratorService urlGeneratorService, ILogger<EncodeService> logger) : IEncodeService
    {
        private readonly IUrlGeneratorService _urlGeneratorService = urlGeneratorService;
        private readonly ILogger<EncodeService> _logger = logger;

        public string Encode(string originalUrl)
        {
            _logger.LogInformation($"Start encode url: {nameof(EncodeService)} - {DateTimeOffset.Now}");
            string shortUrl = _urlGeneratorService.GenerateUrl(originalUrl);
            _logger.LogInformation($"End encode url: {nameof(EncodeService)} - {DateTimeOffset.Now}");
            return shortUrl;
        }
    }
}
