namespace UrlSaver.Domain.Entities
{
    public class UrlModel
	{
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ExpiredDate { get; set; }
	}
}
