using Data.Interface.Models;
using HealthyFoodWeb.Models.Store;

namespace HealthyFoodWeb.Services.IServices
{
    public interface IStoreCatalogueService
    {
        List<StoreItem> GetAllItems();
        List<Manufacturer> GetAllManufacturers();
        void AddStoreItem(StoreItemViewModel viewModel);
        StoreCatalogueViewModel CreateStoreViewModel(int page, int perPage);

        StoreItemViewModel GetItemFromCatalogViewMode(int id);
        void UpdateItemManufacturer(int id,  string manufacturer);

        void UpdateItemImgNamePrice(int id, string newname, decimal newprice, string newimg);
    }
}
