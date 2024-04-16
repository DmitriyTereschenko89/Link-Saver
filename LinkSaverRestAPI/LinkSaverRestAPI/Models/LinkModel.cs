namespace LinkSaverRestAPI.Models
{
	public record class LinkModel
	{
		public Guid Id { get; init; }
		public string OriginalUrl { get; init; }
		public string ShortUrl { get; init; }
		public DateOnly CreatedDate { get; init; }
		public DateOnly ExpiredDate { get; init; }
	}
}
