using Data.Interface.Models;
using Data.Interface.Repositories;
using Data.Sql.Repositories;
using HealthyFoodWeb.Models;
using HealthyFoodWeb.Services.Helpers;
using HealthyFoodWeb.Services.IServices;
using System;

namespace HealthyFoodWeb.Services
{
    public class CartService : ICartService
    {
        private ICartRepository _cartRepository;
        private IAuthService _authService;
        private IPagginatorService _pagginatorService;
        private ICartTagRepository _cartTagRepository;
        private IWebHostEnvironment _webHostEnvironment;
        private IStoreCatalogueRepository _storeCatalogueRepository;


        public CartService(ICartRepository cartRepository,
            IAuthService authService, IPagginatorService pagginatorService, ICartTagRepository cartTagRepository, IWebHostEnvironment webHostEnvironment, IStoreCatalogueRepository storeCatalogueRepository)
        {
            _cartRepository = cartRepository;
            _authService = authService;
            _pagginatorService = pagginatorService;
            _cartTagRepository = cartTagRepository;
            _webHostEnvironment = webHostEnvironment;
            _storeCatalogueRepository = storeCatalogueRepository;
        }

        public void DeleteFromCart(int id)
        {
            _cartRepository.Remove(id);
        }
        public void UpdateTag(int id, List<string> newTagsNames)
        {
            var cartItem = _cartRepository.GetCartAndTags(id);
            if (cartItem.Tags == null)
            {
                cartItem.Tags = new List<CartTags>();
            }

            var newTags = _cartTagRepository
                .GetAll()
                .Where(x => newTagsNames.Contains(x.Name))
                .ToList();

            cartItem.Tags.RemoveAll(x => true);
            newTags.ForEach(x => cartItem.Tags.Add(x));
            _cartRepository.Update(cartItem);
        }
        public List<Cart> GetAllProduct()
        {
            return _cartRepository.GetAll().ToList();
        }
        public List<Cart> GetCustomerProduct()
        {
            var userId = _authService.GetUser().Id;
            var product = _cartRepository.GetAllWithTags().Where(x => x.Customer != null && x.Customer.Id == userId).ToList();

            return product;
        }
        public ProductCountViewModel GetViewModelForProductCount(string userTag)
        {
            var dataModel = GetCustomerProduct()
                .Where(x => x.Tags != null && x.Tags.Any(x => x.Name == userTag))
                .ToList();

            return new ProductCountViewModel
            {
                TotalProductCount = dataModel.Count,
                ProductNames = dataModel.Select(x => x.Name).ToList(),
            };
        }
        public decimal GetTotalPrice()
        {
            decimal TotalPrice = 0;
            foreach (var product in GetCustomerProduct())
            {
                TotalPrice += product.Price * product.Quantity;
            }
            return TotalPrice;
        }
        public void UpdateQuantityOfProductsUp(int id)
        {
            var cartItem = _cartRepository.GetCartAndTags(id);
            cartItem.Quantity += 1;
            _cartRepository.Update(cartItem);
        }
        public void UpdateQuantityOfProductsDown(int id)
        {
            var cartItem = _cartRepository.GetCartAndTags(id);
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
                _cartRepository.Update(cartItem);
            }
            else
            {
                DeleteFromCart(id);
            }
        }
        public void AddProductInCart(CartItemViewModel viewModel)
        {
            var user = _authService.GetUser();
            var dbCartModel = new Cart()
            {
                Name = viewModel.Name,
                Price = viewModel.Price,
                Customer = user,
                ImgUrl = "Temp",
                Tags = new List<CartTags> { },
            };

            _cartRepository.Add(dbCartModel);
            var ext = Path.GetExtension(viewModel.ImgUrlFile.FileName);
            var fileName = $"product-{dbCartModel.Id}{ext}";
            var path = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "images",
                "products",
                fileName);

            using (var fs = File.Create(path))
            {
                viewModel.ImgUrlFile.CopyTo(fs);
            }

            dbCartModel.ImgUrl = $"/images/products/{fileName}";
            _cartRepository.Update(dbCartModel);
        }
        public void AddProductInCartFromCatalog(int id)
        {
            var user = _authService.GetUser();
            var productFromCatalogDB = _storeCatalogueRepository.Get(id);

            var cartItemsNames = GetCustomerProduct().Select(x => x.Name).ToList();
            var cartItems = GetCustomerProduct().ToList();

            if (cartItemsNames.Contains(productFromCatalogDB.Name))
            {
                var cartItemId = cartItems.Where(x => productFromCatalogDB.Name == x.Name)
                    .FirstOrDefault().Id;
                UpdateQuantityOfProductsUp(cartItemId);
            }
            else
            {
                var dbCartModel = new Cart()
                {
                    Name = productFromCatalogDB.Name,
                    Price = productFromCatalogDB.Price,
                    Customer = user,
                    ImgUrl = productFromCatalogDB.ImageUrl
                };
                _cartRepository.Add(dbCartModel);
            }
        }
        public PagginatorViewModel<CartItemViewModel> GetCartsForPaginator(int page, int perPage)
        {
            var viewModel = _pagginatorService
                            .GetPaginatorViewModel(
                                page,
                                perPage,
                                GetCustomerProduct,
                                BuildViewModelFromDbModel,
                                _cartRepository);

            return viewModel;
        }
        private CartItemViewModel BuildViewModelFromDbModel(Cart x)
        {
            var cartDb = _cartRepository.GetCartAndTags(x.Id);
            var tags = _cartTagRepository.GetAll().Select(x => x.Name).ToList();
            return new CartItemViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price * x.Quantity,
                ImgUrl = x.ImgUrl,
                AvailableTags = tags,
                Tags = cartDb.Tags.Select(x => x.Name).ToList(),
                Quantity = x.Quantity,
            };
        }
        public CartItemViewModel GetCartViewModel(int id)
        {
            var cartDb = _cartRepository.GetCartAndTags(id);
            var tags = _cartTagRepository.GetAll().Select(x => x.Name).ToList();
            return new CartItemViewModel
            {
                Id = cartDb.Id,
                Name = cartDb.Name,
                Price = cartDb.Price * cartDb.Quantity,
                ImgUrl = cartDb.ImgUrl,
                AvailableTags = tags,
                Tags = cartDb.Tags.Select(x => x.Name).ToList(),
                Quantity = cartDb.Quantity,
            };
        }
    }
}

