using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Infrastructure.Services
{
    public class UrlService(IUrlRepository urlRepository) : IUrlService
    {
        private readonly IUrlRepository _urlRepository = urlRepository;
        public async Task<UrlModel> GetUrlAsync(string url)
        {
            return await _urlRepository.GetUrlAsync(url);
        }

        public async Task SaveUrlAsync(UrlModel urlModel)
        {
            await _urlRepository.SaveUrlAsync(urlModel);
        }
    }
}
