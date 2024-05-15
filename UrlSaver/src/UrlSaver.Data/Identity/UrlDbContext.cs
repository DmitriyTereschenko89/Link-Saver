using Microsoft.EntityFrameworkCore;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Data.Identity
{
    public class UrlDbContext : DbContext
    {
        public UrlDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<UrlModel> Urls { get; set; }
    }
}
