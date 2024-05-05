using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Data.Identity
{
    public class UrlRepository(UrlDbContext context, ILogger<UrlRepository> logger) : IUrlRepository
    {
        private readonly UrlDbContext _context = context;
<<<<<<< HEAD:UrlSaver/src/UrlSaver.Infrastructure/Identity/UrlRepository.cs
        private readonly ILogger<UrlRepository> _logger = logger;
        
        public async Task<UrlModel> GetUrlModelAsync(string url)
=======

        public async Task<UrlModel> GetUrlAsync(string url)
>>>>>>> develop:UrlSaver/src/UrlSaver.Data/Identity/UrlRepository.cs
        {
            _logger.LogInformation($"Get url model: {nameof(UrlRepository)} - {DateTimeOffset.Now}");
            return await _context.Urls.FirstOrDefaultAsync(x => x.OriginalUrl == url || x.ShortUrl == url);
        }

        public async Task SaveUrlAsync(UrlModel urlModel)
        {
            _logger.LogInformation($"Save the url model: {nameof(UrlRepository)} - {DateTimeOffset.Now}");
            _context.Urls.Add(urlModel);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"End saving: {nameof(UrlRepository)} - {DateTimeOffset.Now}");
        }
    }
}
