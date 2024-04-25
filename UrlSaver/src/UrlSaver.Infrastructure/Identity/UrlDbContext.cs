using Microsoft.EntityFrameworkCore;

using UrlSaver.Domain.Entities;

namespace UrlSaver.Infrastructure.Identity
{
    public class UrlDbContext(DbContextOptions<UrlDbContext> options) : DbContext(options)
    {
        public DbSet<UrlModel> Urls { get; set; }
    }
}
