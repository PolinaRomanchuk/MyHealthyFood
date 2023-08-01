using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class GameCategoryRepository : BaseRepository<GameCategory>, IGameCategoryRepository
    {
        public GameCategoryRepository(WebContext webContext) : base(webContext)
        {
        }

        public GameCategory Get(string name)
        {
            return _dbSet.SingleOrDefault(x => x.Name == name);
        }
    }
}

