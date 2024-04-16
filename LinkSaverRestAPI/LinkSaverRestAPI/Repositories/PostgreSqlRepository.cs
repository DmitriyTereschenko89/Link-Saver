using LinkSaverRestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkSaverRestAPI.Repositories
{
	public class PostgreSqlRepository : IDataRepository
	{
		private readonly PostgreSqlDataContext dataContext;

		public PostgreSqlRepository(PostgreSqlDataContext dataContext)
		{
			this.dataContext = dataContext;
		}

		public async Task<LinkModel> Get(Guid id)
		{
			var linkModel = await dataContext.Links.FirstOrDefaultAsync(link => link.Id == id);
			if (linkModel is null)
			{
				return null;
			}
			return linkModel;
		}

		public async Task<bool> Push(LinkModel link)
		{
			var linkModel = await dataContext.Links.FirstOrDefaultAsync(l => l.OriginalUrl == link.OriginalUrl);
			if (linkModel is null)
			{
				return false;
			}
			dataContext.Links.Add(link);
			await dataContext.SaveChangesAsync();
			return true;
		}
	}
}
