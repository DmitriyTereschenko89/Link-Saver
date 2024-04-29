using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Infrastructure.Services
{
    public class UrlRepositoryService(IUrlRepository urlRepository) : IUrlRepositoryService
    {
        private readonly IUrlRepository _urlRepository = urlRepository;
        public async Task<UrlModel> GetUrlModelAsync(string url)
        {
            return await _urlRepository.GetUrlModelAsync(url);
        }

        public async Task SaveUrlModelAsync(UrlModel urlModel)
        {
            await _urlRepository.SaveUrlModelAsync(urlModel);
        }
    }
}
