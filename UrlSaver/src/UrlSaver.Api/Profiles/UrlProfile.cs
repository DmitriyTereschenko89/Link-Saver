using AutoMapper;
using UrlSaver.Api.DataTransferObjects;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Api.Profiles
{
    public class UrlProfile : Profile
    {
        public UrlProfile()
        {

            _ = CreateMap<UrlDto, UrlModel>()
                .ForMember(dest => dest.OriginalUrl, opt => opt.MapFrom(src => src.Url));
            _ = CreateMap<UrlModel, UrlDto>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.ShortUrl));
            _ = CreateMap<string, UrlDto>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src));
        }
    }
}
