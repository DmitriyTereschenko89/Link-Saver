using UrlSaver.Domain.Common;

namespace UrlSaver.Infrastructure.Services
{
    public class EncodeService(IUrlGeneratorService urlGeneratorService) : IEncodeService
    {
        private readonly IUrlGeneratorService _urlGeneratorService = urlGeneratorService;

        public string Encode(string originalUrl)
        {
            string shortUrl = _urlGeneratorService.GenerateUrl(originalUrl);
            return shortUrl;
        }
    }
}
