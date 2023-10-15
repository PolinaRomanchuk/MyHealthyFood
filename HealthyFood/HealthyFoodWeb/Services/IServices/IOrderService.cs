using HealthyFoodWeb.Models;

namespace HealthyFoodWeb.Services.IServices
{
    public interface IOrderService
    {
        void CreateOrder(OrderViewModel orderViewModel);
    }
}
