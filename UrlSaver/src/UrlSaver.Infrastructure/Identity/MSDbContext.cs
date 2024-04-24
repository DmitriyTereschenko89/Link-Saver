using Microsoft.EntityFrameworkCore;

using UrlSaver.Domain.Entities;

namespace UrlSaver.Infrastructure.Identity
{
    public class MSDbContext(DbContextOptions<MSDbContext> options) : DbContext(options)
    {
        public DbSet<UrlModel> Urls { get; set; }
    }
}
