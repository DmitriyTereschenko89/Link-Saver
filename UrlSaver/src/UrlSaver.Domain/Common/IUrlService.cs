using UrlSaver.Domain.Entities;

namespace UrlSaver.Domain.Common
{
    public interface IUrlService
    {
        Task<string> GetOriginalUrlAsync(string key);
        Task SaveUrlAsync(UrlModel originalUrl);
    }
}
