namespace linksaver.domain.Services
{
    public class LinkGeneratorService : ILinkGeneratorService
    {
        private const string Base58Code = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
        private const int MaxLengthShortUrl = 7;
        private const int Base58MaxCodeLength = 58;

        public string GenerateUrl(string originalUrl)
        {
            ulong urlHash = (ulong)originalUrl.GetHashCode();
            List<int> charCodes = [];
            Random rnd = new();
            while (urlHash != 0)
            {
                charCodes.Add((int)(urlHash % Base58MaxCodeLength));
                urlHash /= Base58MaxCodeLength;
            }
            while (charCodes.Count < MaxLengthShortUrl)
            {
                charCodes.Add(rnd.Next() % Base58MaxCodeLength);
            }
            int differenceLength = MaxLengthShortUrl - charCodes.Count;
            for (int count = 0; count < differenceLength; ++count)
            {
                int index = rnd.Next() % MaxLengthShortUrl;
                if (index < Base58MaxCodeLength - 1)
                {
                    charCodes[index] += charCodes[index + 1];
                }
                else
                {
                    charCodes[index % MaxLengthShortUrl] += charCodes[index % (MaxLengthShortUrl - 1)];
                }
                charCodes[index] %= Base58MaxCodeLength;
            }
            charCodes = charCodes[..MaxLengthShortUrl];
            List<char> shortUrlList = [];
            foreach (int code in charCodes)
            {
                shortUrlList.Add(Base58Code[code]);
            }
            shortUrlList = [.. shortUrlList.OrderBy(x => rnd.Next())];
            return string.Join("", shortUrlList);
        }
    }
}
