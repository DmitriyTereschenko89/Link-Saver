namespace UrlSaver.Domain.Common
{
    public interface IUrlGeneratorService
    {
        string GenerateUrl(string originalUrl);
    }
}
