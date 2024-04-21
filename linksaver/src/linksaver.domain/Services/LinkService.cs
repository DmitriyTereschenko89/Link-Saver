using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using linksaver.data.Repositories;
using linksaver.domain.DTO_s;

using Microsoft.VisualBasic;

namespace linksaver.domain.Services
{
    public class LinkService : ILinkService
    {
        private readonly IEncodeService _encodeService;
        private readonly IRepository _repository;

        public LinkService(IEncodeService encodeService, IRepository repository) 
        {
            _encodeService = encodeService;
            _repository = repository;
        }

        public async Task<LinkModelDTO> GetShortLink(string url)
        {
            var linkModel = await Task.Run(() => _repository.GetModel(url));
            return new LinkModelDTO
            {
                OriginalUrl = linkModel.OriginalLink,
                ShortUrl = linkModel.ShortLink,
                LinkLifespan = linkModel.ExpiredDate.DayNumber - linkModel.CreatedDate.DayNumber
            };
        }

        public async Task SaveLink(LinkModelDTO linkModel)
        {
            var shortUrl = await _encodeService.EncodeUrl(linkModel.OriginalUrl);
            await _repository.SaveModel(new data.Models.LinkModel 
            {
                OriginalLink = linkModel.OriginalUrl,
                ShortLink = shortUrl,
                CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                ExpiredDate = DateOnly.FromDayNumber(DateOnly.FromDateTime(DateTime.Now).DayNumber + linkModel.LinkLifespan)
            });
        }
    }
}
