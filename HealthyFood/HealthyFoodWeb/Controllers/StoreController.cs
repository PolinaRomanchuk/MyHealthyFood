using Data.Interface.Models;
using HealthyFoodWeb.Controllers.CustomAuthorizeAttributes;
using HealthyFoodWeb.Models;
using HealthyFoodWeb.Models.Store;
using HealthyFoodWeb.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthyFoodWeb.Controllers
{
    public class StoreController : Controller
    {
        private ICartService _cartService;
        private IUserService _userService;
        private IAuthService _authService;
        private IStoreCatalogueService _storeCatalogueService;

        public StoreController(ICartService cartService, IUserService userService, IAuthService authService, IStoreCatalogueService storeCatalogueService)
        {
            _userService = userService;
            _authService = authService;
            _cartService = cartService;
            _storeCatalogueService = storeCatalogueService;
        }

        public IActionResult storePageCatalogue(int page = 1, int perPage = 10)
        {
            var viewModel = _storeCatalogueService.CreateStoreViewModel(page, perPage);

            return View(viewModel);
        }

        [Authorize]
        public IActionResult CartPage(int page = 1, int perPage = 4)
        {
            var paginatorViewModel = _cartService.GetCartsForPaginator(page, perPage);
            var cartViewModel = new CartViewModel(paginatorViewModel);
            cartViewModel.TotalPrice = _cartService.GetTotalPrice();
            return View(cartViewModel);
        }

        [HttpGet]
        [Authorize]
        [IsHasRole(MyRole.Admin)]
        public IActionResult AddProductInCart()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [IsHasRole(MyRole.Admin)]
        public IActionResult AddProductInCart(CartItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            _cartService.AddProductInCart(viewModel);
            return RedirectToAction("CartPage");
        }

        [Authorize]
        public IActionResult AddProductInCartFromCatalog(int id)
        {
            _cartService.AddProductInCartFromCatalog(id);
            return RedirectToAction("storePageCatalogue");
        }

        public IActionResult UpdateTagInCart(int id)
        {
            var viewModel = _cartService.GetCartViewModel(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateTagInCart(CartItemViewModel cartViewModel)
        {
            _cartService.UpdateTag(cartViewModel.Id, cartViewModel.Tags);
           return RedirectToAction("CartPage");
        }
        public IActionResult UpdateQuantityCartUp(int id)
        {
            _cartService.UpdateQuantityOfProductsUp(id);
            return RedirectToAction("CartPage");
        }
        public IActionResult UpdateQuantityCartDown(int id)
        {
            _cartService.UpdateQuantityOfProductsDown(id);
            return RedirectToAction("CartPage");
        }

        [HttpGet]
        public IActionResult AddProductInCatalogue()
        {
            var manufacturers = _storeCatalogueService
                .GetAllManufacturers();
            var viewModel = new StoreItemViewModel
            {
                AllManufacturers = manufacturers.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Name,
                })
                .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddProductInCatalogue(StoreItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var manufacturers = _storeCatalogueService
                .GetAllManufacturers();
                viewModel.AllManufacturers = manufacturers.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Name,
                })
                    .ToList();
                return View(viewModel);
            }
            _storeCatalogueService.AddStoreItem(viewModel);
            return RedirectToAction("storePageCatalogue");
        }
    }
}
