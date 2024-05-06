using AutoMapper;

using UrlSaver.Domain.Entities;
using UrlSaver.Infrastructure.Entities;

namespace UrlSaver.Api.Profiles
{
    public class UrlProfile : Profile
    {
        public UrlProfile()
        {
            CreateMap<UrlModel, UrlDto>()
                .ForMember(dest => dest.OriginalUrl, opt => opt.MapFrom(src => src.OriginalUrl))
                .ForMember(dest => dest.ShortUrl, opt => opt.MapFrom(src => src.ShortUrl))
                .ForMember(dest => dest.ShortUrlTimeSpan, opt => opt.MapFrom(src => src.ExpiredDate - src.CreatedDate));

            CreateMap<UrlDto, UrlModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.OriginalUrl, opt => opt.MapFrom(src => src.OriginalUrl))
                .ForMember(dest => dest.ShortUrl, opt => opt.MapFrom(src => src.ShortUrl))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTimeOffset.Now))
                .ForMember(dest => dest.ExpiredDate, opt => opt.MapFrom(src => DateTimeOffset.Now + src.ShortUrlTimeSpan));
            
        }
    }
}
