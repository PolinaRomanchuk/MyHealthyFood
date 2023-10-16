using Data.Interface.Models;
using Data.Interface.Repositories;
using HealthyFoodWeb.Models;
using HealthyFoodWeb.Services.IServices;

namespace HealthyFoodWeb.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IAuthService _authService;
        private ICartService _cartService;

        public OrderService(IOrderRepository orderRepository, IAuthService authService, ICartService cartService)
        {
            _orderRepository = orderRepository;
            _authService = authService;
            _cartService = cartService;
        }

        public void CreateOrder(OrderViewModel viewModel)
        {
            //viewModel.Customer = _authService.GetUser().Name;
            

            var dborder = new Order()
            {
                Name = viewModel.Name,
                Address = CreateAddress(viewModel),
                OrderTime = viewModel.OrderTime,
                Comment = viewModel.Comment,
                PaymentMethod = viewModel.PaymentMethod,
                PhoneNumber = viewModel.PhoneNumber,
                Products = _cartService.GetCustomerProduct(),
            };
            _orderRepository.Add(dborder);
        }

        public string CreateAddress(OrderViewModel viewModel)
        {
            var fullAddress = $"{viewModel.Street}, д. {viewModel.House}, " +
                $"п. {viewModel.Doorway}, э. {viewModel.Floor}, кв. {viewModel.Flat}";
            return fullAddress;
        }
    }

}
