using Microsoft.EntityFrameworkCore;

using UrlSaver.Domain.Entities;

namespace UrlSaver.Data.Identity
{
    public class UrlDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<UrlModel> Urls { get; set; }
    }
}
