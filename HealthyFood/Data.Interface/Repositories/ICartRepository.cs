using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        Cart GetByName(string name);
        Cart GetCartAndTags(int id);
        IEnumerable<Cart> GetAllWithTags();
    }
}
