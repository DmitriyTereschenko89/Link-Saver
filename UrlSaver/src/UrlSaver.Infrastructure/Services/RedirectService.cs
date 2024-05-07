using AutoMapper;

using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;
using UrlSaver.Infrastructure.Common;
using UrlSaver.Infrastructure.Entities;

namespace UrlSaver.Infrastructure.Services
{
    public class RedirectService(IUrlRepository urlRepository, IMapper mapper) : IRedirectService
    {
        private readonly IUrlRepository _urlRepository= urlRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<UrlDto> GetUrl(string url)
        {
            UrlModel urlModel = await _urlRepository.GetUrlAsync(url);
            return urlModel is null ? new UrlDto() : _mapper.Map<UrlDto>(urlModel);
        }
    }
}
