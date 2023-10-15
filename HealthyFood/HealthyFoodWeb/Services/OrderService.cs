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
            var dborder = new Order()
            {
                Name = viewModel.Name,
                Address = viewModel.Address,
                OrderTime = viewModel.OrderTime,
                Comment = viewModel.Comment,
                PaymentMethod = viewModel.PaymentMethod,
                PhoneNumber = viewModel.PhoneNumber,
                Products = _cartService.GetCustomerProduct(),
            };
            _orderRepository.Add(dborder);
        }
    }

}
