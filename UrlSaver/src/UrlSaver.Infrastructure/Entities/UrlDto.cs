namespace UrlSaver.Infrastructure.Entities
{
    public record class UrlDto
    {
        public string OriginalUrl { get; init; } = "Does not exist";
        public string ShortUrl { get; init; } = "Does not exist";
        public TimeSpan ShortUrlTimeSpan { get; init; } = TimeSpan.Zero;
    }
}
