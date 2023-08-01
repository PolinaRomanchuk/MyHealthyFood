using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IGameCategoryRepository : IBaseRepository<GameCategory>
    {
        public GameCategory Get(string name);
    }
}
