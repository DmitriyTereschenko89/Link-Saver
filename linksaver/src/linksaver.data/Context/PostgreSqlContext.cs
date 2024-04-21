using linksaver.data.Models;

using Microsoft.EntityFrameworkCore;

namespace linksaver.data.Context
{
    public class PostgreSqlContext : DbContext
    {
        public DbSet<LinkModel> Links { get; set; }
    }
}
