﻿using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IStoreCatalogueRepository : IBaseRepository<StoreItem>
    {
        StoreItem GetByName(string name);
        List<StoreItem> GetItemsWithManufacturer();
        StoreItem GetItemWithManufacturer(int id);
        void UpdateNamePriceImgItem(int id, string name, decimal price, string img);
    }
}
