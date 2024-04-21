namespace linksaver.domain.Services
{
    public interface IEncodeService
    {
        Task<string> EncodeUrl(string url);
    }
}
