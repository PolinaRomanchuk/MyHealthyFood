using HealthyFoodWeb.Models;
using HealthyFoodWeb.Models.Games;
using HealthyFoodWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HealthyFoodWeb.Controllers.API
{
    [ApiController]
    [Route("/api/store")]
    public class StoreApiController : Controller
    {
        private ICartService _cartService;
        public StoreApiController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Route("ProductCount")]
        public ProductCountViewModel GetProductsByTagCount(string userTag)
        {
            var viewModel = _cartService.GetViewModelForProductCount(userTag);
            return viewModel;
        }
    }
}
