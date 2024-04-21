using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace linksaver.data.Models
{
    [Table("links")]
    public class LinkModel
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        [Column("original_url")]
        public string OriginalLink { get; set; }
        [Column("short_link")]
        public string ShortLink { get; set; }
        [Column("created_date")]
        public DateOnly CreatedDate { get; set; }
        [Column("expired_date")]
        public DateOnly ExpiredDate { get; set; }
    }
}
