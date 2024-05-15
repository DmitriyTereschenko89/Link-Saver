using System.ComponentModel.DataAnnotations;

namespace UrlSaver.Domain.Entities
{
    public class UrlModel
	{
        [Key]
        public string ShortUrl { get; set; }
        public string OriginalUrl { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ExpiredDate { get; set; }
	}
}
