using System;
using System.Text.RegularExpressions;

namespace UrlSaver.Api.Extentions
{
    public static class UrlValidationExtention
    {
        public static bool ValidateUrl(this string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) &&
                    (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
