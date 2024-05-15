using AutoMapper;
using UrlSaver.Api.DataTransferObjects;
using UrlSaver.Domain.Entities;

namespace UrlSaver.Api.Profiles
{
    public class UrlProfile : Profile
    {
        public UrlProfile() 
        {
            
            CreateMap<UrlDto, UrlModel>()
                .ForMember(dest => dest.OriginalUrl, opt => opt.MapFrom(src => src.Url));
        }
    }
}
