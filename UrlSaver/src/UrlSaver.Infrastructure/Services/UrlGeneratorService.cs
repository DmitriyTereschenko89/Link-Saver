using System.Configuration;

using UrlSaver.Domain.Common;

namespace UrlSaver.Infrastructure.Services
{
    public class UrlGeneratorService : IUrlGeneratorService
    {
        public string GenerateUrl(string originalUrl)
        {
            string codingSequence = ConfigurationManager.AppSettings["CodingSequence"];
            int urlMaxLength = int.Parse(ConfigurationManager.AppSettings["UrlMaxLength"]);
            int codingSequenceLength = codingSequence.Length;
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
                shortUrlList.Add(codingSequence[code]);
            }

            shortUrlList = [.. shortUrlList.OrderBy(x => rnd.Next())];
            return string.Join("", shortUrlList);
        }
    }
}
