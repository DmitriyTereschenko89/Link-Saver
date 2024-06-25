namespace UrlSaver.Domain.Entities
{
    public class CacheOptions
    {
        public int Size { get; set; }
        public long SizeLimit { get; set; }
        public int SlidingExpirationSeconds { get; set; }
        public int AbsoluteExpirationSeconds { get; set; }
    }
}
