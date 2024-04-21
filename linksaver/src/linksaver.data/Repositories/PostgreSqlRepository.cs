using linksaver.data.Context;
using linksaver.data.Models;

using Microsoft.EntityFrameworkCore;

namespace linksaver.data.Repositories
{
    public class PostgreSqlRepository(PostgreSqlContext postgreSqlContext) : IRepository
    {
        private readonly PostgreSqlContext _postgreSqlContext = postgreSqlContext;

        public async Task<LinkModel> GetModel(string url)
        {
            var linkModel = await _postgreSqlContext.Links.FirstOrDefaultAsync();
            return linkModel ?? null;
        }

        public async Task SaveModel(LinkModel model)
        {
            model.Id = Guid.NewGuid();
            _postgreSqlContext.Links.Add(model);
            await _postgreSqlContext.SaveChangesAsync();
        }
    }
}
