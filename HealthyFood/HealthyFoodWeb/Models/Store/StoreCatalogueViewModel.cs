namespace HealthyFoodWeb.Models.Store
{
    public class StoreCatalogueViewModel
    {
        public PagginatorViewModel<StoreItemViewModel> ItemsPagginator { get; set; }
        public List<ManufacturerViewModel> Manufacturer { get; set; }

    }
}
