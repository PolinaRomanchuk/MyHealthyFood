using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Repositories
{
    public class ManufacturerRepository : BaseRepository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(WebContext webContext) : base(webContext) { }
        public Manufacturer GetFirst()
        {
            return _dbSet.FirstOrDefault();
        }

        public Manufacturer GetByName(string name)
        {
            return _dbSet.FirstOrDefault(x => x.Name == name);
        }

        public List<Manufacturer> GetManufacturerWithStoreItems()
        {
            return _dbSet
               .Include(x => x.StoreItems).ToList();
        }
    }
}
