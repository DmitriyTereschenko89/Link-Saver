using UrlSaver.Domain.Entities;

namespace UrlSaver.Domain.Common
{
    public interface IUrlService
    {
        Task<UrlModel> GetUrlAsync(string url);
        Task SaveUrlAsync(UrlModel urlModel);
    }
}
