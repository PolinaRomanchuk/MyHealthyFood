using Data.Interface.DataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Repositories
{
    public class StoreCatalogueRepository : BaseRepository<StoreItem>, IStoreCatalogueRepository
    {
        public StoreCatalogueRepository(WebContext webContext) : base(webContext) { }


        public StoreItem GetByName(string name)
        {
            return _dbSet.FirstOrDefault(x => x.Name == name);
        }

        public List<StoreItem> GetItemsWithManufacturer()
        {
            return _dbSet
                .Include(x => x.Manufacturer)
                .ToList();
               
        }

        public StoreItem GetItemWithManufacturer(int id)
        {
            return _dbSet.Include(x => x.Manufacturer).SingleOrDefault(x => x.Id == id);
        }

        public void UpdateItem(int id, string name, decimal price, string img, Manufacturer manufacturer)
        {
            var item = _dbSet.SingleOrDefault(x => x.Id == id);
            item.Name = name;
            item.Price = price;
            item.ImageUrl = img;
            item.Manufacturer = manufacturer;
            _webContext.SaveChanges();
        }

        public override PaginatorData<StoreItem> GetPaginator(int page, int perPage)
        {
            var initialSource = _dbSet.Include(x => x.Manufacturer);
            return base.GetPaginator(initialSource, page, perPage);
        }
    }
}
