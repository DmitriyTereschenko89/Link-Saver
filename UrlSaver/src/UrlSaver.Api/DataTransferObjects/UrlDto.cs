using System.ComponentModel.DataAnnotations;
using UrlSaver.Api.Attributes;

namespace UrlSaver.Api.DataTransferObjects
{
    public record class UrlDto
    {
        [Required]
        [UrlValidation(ErrorMessage = "Url is invalid.")]
        public string Url { get; init; }
    }
}
