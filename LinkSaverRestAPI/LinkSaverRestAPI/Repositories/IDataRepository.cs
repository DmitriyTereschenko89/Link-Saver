using LinkSaverRestAPI.Models;

namespace LinkSaverRestAPI.Repositories
{
	public interface IDataRepository
	{
		Task<bool> Push(LinkModel link);
		Task<LinkModel> Get(Guid id);
	}
}
