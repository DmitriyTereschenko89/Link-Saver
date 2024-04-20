using linksaver.data.Models;

namespace linksaver.data.Repositories
{
    public interface IRepository
	{
        LinkModel GetModel(Guid id);
        LinkModel GetModel(string url);
	}
}
