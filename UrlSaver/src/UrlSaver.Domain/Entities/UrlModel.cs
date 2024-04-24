namespace UrlSaver.Domain.Entities
{
    public class UrlModel
	{
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateOnly CreatedDate { get; set; }
        public DateOnly ExpiredDate { get; set; }
	}
}
