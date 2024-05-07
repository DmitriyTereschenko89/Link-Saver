using System;
using System.Text.RegularExpressions;

namespace UrlSaver.Api.Extentions
{
    public static class UrlValidationExtention
    {
        private const string pattern =
            @"^*((https?|ftps?):\/\/)?(?:[a-zA-Z0-9]" +
            @"(?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,}" +
            @"(?::(?:0|[1-9]\d{0,3}|[1-5]\d{4}|6[0-4]\d{3}" +
            @"|65[0-4]\d{2}|655[0-2]\d|6553[0-5]))?" +
            @"(?:\/(?:[-a-zA-Z0-9@%_\+.~#?&=]+\/?)*)?$";
        public static bool ValidateUrl(this string url)
        {
            var urlRegex = new Regex(pattern, RegexOptions.IgnoreCase);
            return urlRegex.IsMatch(Uri.UnescapeDataString(url));
        }
    }
}
