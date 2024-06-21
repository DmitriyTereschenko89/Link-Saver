namespace UrlSaver.Domain.Common
{
    public interface ICacheService
    {
        Task<string> GetOrCreateAsync(string key, Func<Task<string>> createKey);
    }
}
