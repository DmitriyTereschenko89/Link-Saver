using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using linksaver.domain.DTO_s;

namespace linksaver.domain.Services
{
    public interface ILinkService
    {
        Task SaveLink(LinkModelDTO linkModel);
        Task<LinkModelDTO> GetShortLink(string url);
    }
}
