using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByName(string name);
        User GetByNameAndPassword(string login, string password);
        void RemoveByName(string name);
        User GetFirst();
    }
}
