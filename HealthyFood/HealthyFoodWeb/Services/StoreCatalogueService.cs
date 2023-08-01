using Data.Interface.Models;
using Data.Interface.Repositories;
using Data.Sql.Repositories;
using HealthyFoodWeb.Models;
using HealthyFoodWeb.Models.Store;
using HealthyFoodWeb.Services.Helpers;
using HealthyFoodWeb.Services.IServices;

namespace HealthyFoodWeb.Services
{
    public class ScopedRegistrationAttribute : Attribute { }

    public class StoreCatalogueService : IStoreCatalogueService
    {
        
        private IStoreCatalogueRepository _catalogueRepository;
        private IManufacturerRepository _manufacturerRepository;
        private IPagginatorService _pagginatorService;

        [ScopedRegistration]
        public StoreCatalogueService(IStoreCatalogueRepository catalogueRepositories, IManufacturerRepository manufacturerRepository, IPagginatorService pagginatorService)
        {
            _catalogueRepository = catalogueRepositories;
            _manufacturerRepository = manufacturerRepository;
            _pagginatorService = pagginatorService;
        }

        public List<StoreItem> GetAllItems()
        {
            return _catalogueRepository.GetItemsWithManufacturer();
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            return _manufacturerRepository.GetAll().ToList();
        }

        public void AddStoreItem(StoreItemViewModel viewModel) 
        {
            var manufacturer = _manufacturerRepository.GetByName(viewModel.Manufacturer);
            var dbCartModel = new StoreItem()
            {
                Name = viewModel.Name,
                Price = viewModel.Price,
                ImageUrl = viewModel.Img,
                Manufacturer = manufacturer,
            };

            _catalogueRepository.Add(dbCartModel);
        }

        public StoreCatalogueViewModel CreateStoreViewModel(int page, int perPage)
        {
            var viewModel = new StoreCatalogueViewModel();
            viewModel.ItemsPagginator = GetStoreItemsForPaginator(page, perPage);
            viewModel.Manufacturer = GetAllManufacturers()
                .Select(x => new ManufacturerViewModel
                {
                    Name = x.Name,
                }).ToList();

            return viewModel;

        }

        public PagginatorViewModel<StoreItemViewModel> GetStoreItemsForPaginator(int page, int perPage)
        {
            var viewModel = _pagginatorService
                .GetPaginatorViewModel(
                    page,
                    perPage,
                    BuildViewModelFromDbModel,
                    _catalogueRepository);

            return viewModel;
        }

        private StoreItemViewModel BuildViewModelFromDbModel(StoreItem item)
        {
            return new StoreItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Img = item.ImageUrl,
                Manufacturer = item.Manufacturer.Name
            };
        }

    }
}

