using linksaver.data.Models;

namespace linksaver.data.Repositories
{
    public interface IRepository
	{
        Task<LinkModel> GetModel(string url);
        Task SaveModel(LinkModel model);
	}
}
