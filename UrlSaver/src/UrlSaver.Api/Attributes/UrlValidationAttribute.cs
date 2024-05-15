using System.ComponentModel.DataAnnotations;
using UrlSaver.Api.Extentions;

namespace UrlSaver.Api.Attributes
{
    public class UrlValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string url = value as string;

            return url.ValidateUrl();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return IsValid(value) ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}
