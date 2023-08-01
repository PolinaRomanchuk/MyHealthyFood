using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class CartTagsRepository : BaseRepository<CartTags>, ICartTagRepository
    {
        public CartTagsRepository(WebContext webContext) : base(webContext)
        {
        }

        public CartTags Get(string name)
        {
            return _dbSet.SingleOrDefault(x => x.Name == name);
        }
    }
}
