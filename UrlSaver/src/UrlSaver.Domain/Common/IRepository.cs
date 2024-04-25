using UrlSaver.Domain.Entities;

namespace UrlSaver.Domain.Common
{
    public interface IUrlRepository
    {
        Task<UrlModel> GetUrlModelAsync(string url);
        Task SaveUrlModelAsync(UrlModel urlModel);
    }
}
