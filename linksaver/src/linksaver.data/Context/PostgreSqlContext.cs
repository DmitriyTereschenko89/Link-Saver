using linksaver.data.Models;

using Microsoft.EntityFrameworkCore;

namespace linksaver.data.Context
{
    public class PostgreSqlContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=linksaverdb;");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<LinkModel> Links { get; set; }
    }
}
