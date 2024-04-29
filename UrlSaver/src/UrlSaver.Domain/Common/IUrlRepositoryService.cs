using UrlSaver.Domain.Entities;

namespace UrlSaver.Domain.Common
{
    public interface IUrlRepositoryService
    {
        Task<UrlModel> GetUrlModelAsync(string url);
        Task SaveUrlModelAsync(UrlModel urlModel);
    }
}
