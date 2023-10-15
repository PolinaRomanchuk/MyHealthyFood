using Azure;
using Data.Interface.Models;
using Data.Interface.Repositories;
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
        private IOrderService _orderService;

        public StoreController(ICartService cartService, IUserService userService, IAuthService authService, IStoreCatalogueService storeCatalogueService, IOrderService orderservice)
        {
            _userService = userService;
            _authService = authService;
            _cartService = cartService;
            _storeCatalogueService = storeCatalogueService;
            _orderService = orderservice;
        }
       
        [Authorize]
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
        [IsHasRole(MyRole.Admin)]
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
        [IsHasRole(MyRole.Admin)]
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

        [HttpGet]
        [Authorize]
        [IsHasRole(MyRole.Admin)]
        public IActionResult UpdateStoreCatalogue(int id)
        {
            var viewModel = _storeCatalogueService.GetItemFromCatalogViewMode(id);
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

        [HttpPost]
        [Authorize]
        [IsHasRole(MyRole.Admin)]
        public IActionResult UpdateStoreCatalogue(StoreItemViewModel viewModel)
        {
            _storeCatalogueService.UpdateItemImgNamePrice(viewModel.Id, viewModel.Name, viewModel.Price, viewModel.Img);
            _storeCatalogueService.UpdateItemManufacturer(viewModel.Id, viewModel.Manufacturer);
            return RedirectToAction("storePageCatalogue");
        }


        [HttpGet]
        public IActionResult Ordering()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ordering(OrderViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            _orderService.CreateOrder(viewModel);
            return RedirectToAction("OrderIsDone");
        }
    }
}
