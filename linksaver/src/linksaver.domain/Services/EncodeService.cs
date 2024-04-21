namespace linksaver.domain.Services
{
    public class EncodeService(ILinkGeneratorService linkGeneratorService) : IEncodeService
    {
        private readonly ILinkGeneratorService _linkGeneratorService = linkGeneratorService;

        public async Task<string> EncodeUrl(string url)
        {
            return await Task.Run(() => _linkGeneratorService.GenerateUrl(url));
        }
    }
}
