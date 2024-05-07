namespace UrlSaver.Api.Extentions
{
    public static class UrlValidationExtention
    {
        public static bool ValidateUrl(this string url)
        {
            url = Uri.UnescapeDataString(url);
            if (url.StartsWith("www."))
            {
                url = string.Concat("https://", url);
            }

            return Uri.TryCreate(Uri.UnescapeDataString(url), UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps ||
                    uriResult.Scheme == Uri.UriSchemeFtp || uriResult.Scheme == Uri.UriSchemeFtps);
        }
    }
}
