using Microsoft.EntityFrameworkCore;

using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Infrastructure.Identity
{
    public class MSSqlRepository(MSDbContext context) : IMSSqlRepository
    {
        private readonly MSDbContext _context = context;
        
        public async Task<UrlModel> GetUrlModelAsync(string url)
        {
            return await _context.Urls.FirstOrDefaultAsync(x => x.OriginalUrl == url || x.ShortUrl == url);
        }

        public async Task SaveUrlModelAsync(UrlModel urlModel)
        {
            _context.Urls.Add(urlModel);
            await _context.SaveChangesAsync();
        }
    }
}
