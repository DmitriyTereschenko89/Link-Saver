using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Infrastructure.Identity
{
    public class UrlRepository(UrlDbContext context, ILogger<UrlRepository> logger) : IUrlRepository
    {
        private readonly UrlDbContext _context = context;
        private readonly ILogger<UrlRepository> _logger = logger;
        
        public async Task<UrlModel> GetUrlModelAsync(string url)
        {
            _logger.LogInformation($"Try to get url model - {DateTimeOffset.Now}");
            return await _context.Urls.FirstOrDefaultAsync(x => x.OriginalUrl == url || x.ShortUrl == url);
        }

        public async Task SaveUrlModelAsync(UrlModel urlModel)
        {
            _logger.LogInformation($"Saving the url model - {DateTimeOffset.Now}");
            _context.Urls.Add(urlModel);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"End saving - {DateTimeOffset.Now}");
        }
    }
}
