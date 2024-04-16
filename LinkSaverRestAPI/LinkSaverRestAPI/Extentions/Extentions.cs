using LinkSaverRestAPI.DatabaseOptions;

namespace LinkSaverRestAPI.Extentions
{
    public static class Extentions
    {
        public static string GetConnectionString(this PostgreSqlOptions postgreSqlOptions)
        {
            return $"{postgreSqlOptions.ConnectionString}";
        }
    }
}
