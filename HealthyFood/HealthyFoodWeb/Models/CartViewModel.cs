namespace HealthyFoodWeb.Models
{
    public class CartViewModel
    {
        public CartViewModel(PagginatorViewModel<CartItemViewModel> pagginatorViewModel)
        {
            PagginatorViewModel = pagginatorViewModel;
        }
        public PagginatorViewModel<CartItemViewModel> PagginatorViewModel { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
