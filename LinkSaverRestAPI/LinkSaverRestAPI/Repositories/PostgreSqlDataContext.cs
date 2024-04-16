using LinkSaverRestAPI.DatabaseOptions;
using LinkSaverRestAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace LinkSaverRestAPI.Repositories
{
	public class PostgreSqlDataContext: DbContext
	{
		private readonly IConfiguration configuration;
		public PostgreSqlDataContext(IConfiguration configuration, DbContextOptions<PostgreSqlDataContext> optionsBuilder) : base(optionsBuilder)
		{
			this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var dbConnectionOptions = configuration.GetSection("PostgreSql").Get<PostgreSqlOptions>() ?? throw new SettingsPropertyNotFoundException($"{nameof(PostgreSqlOptions)} config is not configured");
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseNpgsql(dbConnectionOptions.ConnectionString);
			}
			base.OnConfiguring(optionsBuilder);
		}

		public DbSet<LinkModel> Links { get; set; }
	}
}
