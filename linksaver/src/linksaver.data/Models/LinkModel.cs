namespace linksaver.data.Models
{
    public class LinkModel
    {
        public Guid Id { get; set; }
        public string OriginalLink { get; set; }
        public string ShortLink { get; set; }
        public DateOnly CreatedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
