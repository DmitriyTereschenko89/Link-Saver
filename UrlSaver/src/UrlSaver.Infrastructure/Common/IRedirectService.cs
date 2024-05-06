using UrlSaver.Infrastructure.Entities;

namespace UrlSaver.Infrastructure.Common
{
    public interface IRedirectService
    {
        Task<UrlDto> GetUrl(string url);
    }
}
