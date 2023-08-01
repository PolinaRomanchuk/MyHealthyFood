using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IManufacturerRepository : IBaseRepository<Manufacturer>
    {
        Manufacturer GetFirst();
        Manufacturer GetByName(string name);
    }
}
