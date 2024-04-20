using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linksaver.domain.Services
{
	public interface ILinkGeneratorService
	{
        string GenerateUrl(string originalUrl);
	}
}
