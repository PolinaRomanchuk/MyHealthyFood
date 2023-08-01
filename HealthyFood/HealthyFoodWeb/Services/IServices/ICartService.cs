using Data.Interface.Models;
using HealthyFoodWeb.Models;

namespace HealthyFoodWeb.Services.IServices
{
    public interface ICartService
    {
        List<Cart> GetAllProduct();
        List<Cart> GetCustomerProduct();
        void DeleteFromCart(int id);
        void AddProductInCart(CartItemViewModel viewModel);
        void AddProductInCartFromCatalog(int id);
        PagginatorViewModel<CartItemViewModel> GetCartsForPaginator(int page, int perPage);
        decimal GetTotalPrice();
        CartItemViewModel GetCartViewModel(int id);
        void UpdateTag(int id, List<string> newTagsNames);
        ProductCountViewModel GetViewModelForProductCount(string userTag);
        void UpdateQuantityOfProductsUp(int id);
        void UpdateQuantityOfProductsDown(int id);
    }
}
