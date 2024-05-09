using Microsoft.EntityFrameworkCore;
using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Data.Identity
{
    public class UrlRepository(UrlDbContext context) : IUrlRepository
    {
        private readonly UrlDbContext _context = context;

        public async Task<UrlModel> GetUrlAsync(string url)
        {
            return await _context.Urls.FirstOrDefaultAsync(x => x.OriginalUrl == url || x.ShortUrl == url);
        }

        public async Task SaveUrlAsync(UrlModel urlModel)
        {
            _context.Urls.Add(urlModel);
            await _context.SaveChangesAsync();
        }
    }
}
