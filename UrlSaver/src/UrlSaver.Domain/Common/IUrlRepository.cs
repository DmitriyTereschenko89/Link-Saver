using UrlSaver.Domain.Entities;

namespace UrlSaver.Domain.Common
{
    public interface IUrlRepository
    {
        Task<string> GetOriginalUrlAsync(string key);
        Task SaveUrlAsync(UrlModel originalUrl);
    }
}
