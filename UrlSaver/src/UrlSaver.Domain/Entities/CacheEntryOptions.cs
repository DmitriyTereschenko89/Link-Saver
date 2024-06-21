namespace UrlSaver.Domain.Entities
{
    public class CacheEntryOptions
    {
        public int SizeLimit { get; set; }
        public int SlidingExpirationSeconds { get; set; }
        public int AbsoluteExpirationSeconds { get; set; }
    }
}
