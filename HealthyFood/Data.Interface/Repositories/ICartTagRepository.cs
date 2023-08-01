using Data.Interface.DataModels;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface ICartTagRepository : IBaseRepository<CartTags>
    {
        CartTags Get(string name);
    }
}
