using Microsoft.EntityFrameworkCore;
using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Data.Identity
{
    public class UrlRepository(UrlDbContext context) : IUrlRepository
    {
        private readonly UrlDbContext _context = context;

        public async Task<string> GetOriginalUrlAsync(string key)
        {
            var originalUrl = await _context.Urls.FirstOrDefaultAsync(x => x.ShortUrl == key && x.ExpiredDate >= DateTimeOffset.UtcNow);

            return originalUrl?.OriginalUrl ?? string.Empty;
        }

        public async Task SaveUrlAsync(UrlModel urlModel)
        {
            _ = _context.Urls.Add(urlModel);
            _ = await _context.SaveChangesAsync();
        }
    }
}
