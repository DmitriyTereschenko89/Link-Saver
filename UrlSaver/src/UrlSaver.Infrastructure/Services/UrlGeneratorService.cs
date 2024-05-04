using Microsoft.Extensions.Options;

using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Infrastructure.Services
{
    public class UrlGeneratorService(IOptions<EncodeOptions> options) : IUrlGeneratorService
    {
        private const string CodingSequence = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
        private readonly IOptions<EncodeOptions> _options = options;
        public string GenerateUrl(string originalUrl)
        {
            int urlMaxLength = _options.Value.UrlMaxLength;
            int codingSequenceLength = CodingSequence.Length;
            uint urlHash = (uint)originalUrl.GetHashCode();
            List<int> charCodes = [];
            Random rnd = new();
            while (urlHash != 0)
            {
                charCodes.Add((int)(urlHash % codingSequenceLength));
                urlHash /= (uint)codingSequenceLength;
            }

            while (charCodes.Count < urlMaxLength)
            {
                charCodes.Add(rnd.Next() % codingSequenceLength);
            }

            int differenceLength = urlMaxLength - charCodes.Count;
            for (int count = 0; count < differenceLength; ++count)
            {
                int index = rnd.Next() % urlMaxLength;
                if (index < codingSequenceLength - 1)
                {
                    charCodes[index] += charCodes[index + 1];
                }
                else
                {
                    charCodes[index % urlMaxLength] += charCodes[index % (urlMaxLength- 1)];
                }

                charCodes[index] %= codingSequenceLength;
            }

            charCodes = charCodes[..urlMaxLength];
            List<char> shortUrlList = [];
            foreach (int code in charCodes)
            {
                shortUrlList.Add(CodingSequence[code]);
            }

            shortUrlList = [.. shortUrlList.OrderBy(x => rnd.Next())];
            return string.Join("", shortUrlList);
        }
    }
}
