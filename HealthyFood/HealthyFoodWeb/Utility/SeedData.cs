using Data.Interface.Models;
using Data.Interface.Repositories;
using Data.Sql.Repositories;

namespace HealthyFoodWeb.Utility
{
    public static class SeedData
    {
        private const int MIN_GAME_COUNT = 20;
        private const int MIN_STORE_COUNT = 5;
        private static Random _random = new Random();

        public static void Seed(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                SeedUsers(scope);
                SeedManufacturer(scope);
                SeedStoreItems(scope);
                SeedCartTags(scope);
                SeedCart(scope);
            }
        }

        private static void SeedCart(IServiceScope scope)
        {
            var cartRepository = scope.ServiceProvider.GetRequiredService<ICartRepository>();
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var cartTagRepository = scope.ServiceProvider.GetRequiredService<ICartTagRepository>();

            if (!cartRepository.Any())
            {
                var user = userRepository.GetFirst();
                var tags = cartTagRepository.GetAll();
                var productdefault = new Cart
                {
                    Name = "Greek salad",
                    Price = 13,
                    Customer = user,
                    ImgUrl = "https://zira.uz/wp-content/uploads/2018/05/grecheskiy-salat-2.jpg",
                    Tags = new List<CartTags> { tags.Random() }
                };
                cartRepository.Add(productdefault);
            }
        }

        private static void SeedCartTags(IServiceScope scope)
        {
            var defaultTags = new List<string> { "Vegetarian", "Vegan", "Lactose-free", "Low-calorie", "Sugar-free" };

            var cartTagsRepository = scope.ServiceProvider
                .GetRequiredService<ICartTagRepository>();

            foreach (var tag in defaultTags)
            {
                if (cartTagsRepository.Get(tag) == null)
                {
                    var cartTagCatalog = new CartTags
                    {
                        Name = tag
                    };
                    cartTagsRepository.Add(cartTagCatalog);
                }
            }
        }

        private static void SeedUsers(IServiceScope scope)
        {
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

            var admin = userRepository.GetByName("Admin");
            if (admin == null)
            {
                admin = new User
                {
                    Name = "Admin",
                    Password = "123",
                    AvatarUrl = "NoAvatar",
                    Role = MyRole.Admin
                };
                userRepository.Add(admin);
            }

            if (admin.Role != MyRole.Admin)
            {
                admin.Role = MyRole.Admin;
                userRepository.Update(admin);
            }
        }

        private static void SeedManufacturer(IServiceScope scope)
        {
            var defaultManufacturers = new List<string> { "Republic of Belarus", "Russian Federation","Poland", "Germany", "Unknown" };
            var manufacturerRepository = scope.ServiceProvider.GetRequiredService<IManufacturerRepository>();

            foreach (var man in defaultManufacturers)
            {
                if (manufacturerRepository.GetByName(man) == null)
                {
                    var manufactList = new Manufacturer
                    {
                        Name = man
                    };
                    manufacturerRepository.Add(manufactList);
                }
            }
        }

        private static void SeedStoreItems(IServiceScope scope)
        {
            var storeCatalogueRepository = scope.ServiceProvider.GetRequiredService<IStoreCatalogueRepository>();
            var manufacturerRep = scope.ServiceProvider.GetRequiredService<IManufacturerRepository>();

            if (storeCatalogueRepository.Count() < MIN_STORE_COUNT)
            {
                var allmanufact = manufacturerRep.GetAll();

                var item1 = new StoreItem
                {
                    Name = "Apple",
                    Price = 2,
                    ImageUrl = "https://5.imimg.com/data5/AK/RA/MY-68428614/apple.jpg",
                    Manufacturer = allmanufact.Random(),
                };
                storeCatalogueRepository.Add(item1);

                var item2 = new StoreItem
                {
                    Name = "Melon",
                    Price = 10,
                    ImageUrl = "https://images.heb.com/is/image/HEBGrocery/004837553-1?jpegSize=150&hei=1400&fit=constrain&qlt=75",
                    Manufacturer = allmanufact.Random(),
                };
                storeCatalogueRepository.Add(item2);

                var item3 = new StoreItem
                {
                    Name = "Watermelon",
                    Price = 12,
                    ImageUrl = "https://i5.walmartimages.com/seo/Fresh-Seedless-Watermelon-Each_e2ec527d-fe7b-4309-9373-186de34557cf.1c562d1a69a2a8f4cb7b5de8f125fc76.jpeg",
                    Manufacturer = allmanufact.Random(),
                };
                storeCatalogueRepository.Add(item3);

                var item4 = new StoreItem
                {
                    Name = "Pineapple",
                    Price = 35,
                    ImageUrl = "https://thumbs.dreamstime.com/b/pineapple-slices-isolated-white-30985039.jpg",
                    Manufacturer = allmanufact.Random(),
                };
                storeCatalogueRepository.Add(item4);

                var item5 = new StoreItem
                {
                    Name = "Champignon",
                    Price = 30,
                    ImageUrl = "https://chefsmandala.com/wp-content/uploads/2018/03/Mushroom-Champignon-White.jpg",
                    Manufacturer = allmanufact.Random(),
                };
                storeCatalogueRepository.Add(item5);
            }
        }
    }
}