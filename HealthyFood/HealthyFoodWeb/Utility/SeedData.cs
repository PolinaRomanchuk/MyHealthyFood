using Data.Interface.Models;
using Data.Interface.Repositories;

namespace HealthyFoodWeb.Utility
{
    public static class SeedData
    {
        private const int MIN_GAME_COUNT = 20;
        private const int MIN_STORE_COUNT = 20;
        private const int MIN_CART_COUNT = 5;
        private static Random _random = new Random();

        public static void Seed(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                SeedUsers(scope);
                SeedManufacturer(scope);
                SeedStoreItems(scope);
                SeedGameCategory(scope);
                SeedGame(scope);
                SeedCart(scope);
                SeedCartTags(scope);
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

            if (cartRepository.Count() < MIN_CART_COUNT)
            {
                var user = userRepository.GetFirst();
                var tags = cartTagRepository.GetAll();

                for (int i = 0; i < MIN_CART_COUNT; i++)
                {
                    var product = new Cart
                    {
                        Name = $"Sup {i}",
                        Price = 10 + i,
                        Customer = user,
                        ImgUrl = "https://korshop.ru/upload/medialibrary/c01/c01f2dae23228a2d4d42eed20544ae2b.jpg",
                        Tags = new List<CartTags> { tags.Random() }
                    };
                    cartRepository.Add(product);
                }
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
            var manufacturerRepository = scope.ServiceProvider.GetRequiredService<IManufacturerRepository>();

            if (!manufacturerRepository.Any())
            {
                var adminManufacturer = new Manufacturer
                {
                    Name = "AdminManufacturer",
                };
                manufacturerRepository.Add(adminManufacturer);
            }
        }

        private static void SeedStoreItems(IServiceScope scope)
        {
            var storeCatalogueRepository = scope.ServiceProvider.GetRequiredService<IStoreCatalogueRepository>();
            var manufacturerRep = scope.ServiceProvider.GetRequiredService<IManufacturerRepository>();

            if (!storeCatalogueRepository.Any())
            {
                var manufacturer = manufacturerRep.GetFirst();
                var adminItem = new StoreItem
                {
                    Name = "Admin",
                    Price = 16,
                    ImageUrl = "NoImage",
                    Manufacturer = manufacturer

                };
                storeCatalogueRepository.Add(adminItem);
            }

            if (storeCatalogueRepository.Count() < MIN_STORE_COUNT)
            {
                var manufacturer = manufacturerRep.GetFirst();

                for (int i = 0; i < MIN_STORE_COUNT; i++)
                {
                    var adminItem = new StoreItem
                    {
                        Name = $"Admin{i}",
                        Price = 1 + i,
                        ImageUrl = "NoImage",
                        Manufacturer = manufacturer

                    };
                    storeCatalogueRepository.Add(adminItem);
                }
            }
        }

        private static void SeedGame(IServiceScope scope)
        {
            var gameRepository = scope.ServiceProvider.GetRequiredService<IGameRepository>();
            var genreRepository = scope.ServiceProvider.GetRequiredService<IGameCategoryRepository>();

            if (gameRepository.Count() < MIN_GAME_COUNT)
            {
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var randomUser = userRepository.GetFirst();
                var genres = genreRepository.GetAll();

                for (int i = 0; i < MIN_GAME_COUNT; i++)
                {
                    var game = new Game
                    {
                        Name = $"RichGame№{i}",
                        Price = 1 + _random.Next(100),
                        CoverUrl = "https://i.imgur.com/eOtEAB7.jpg",
                        Creater = randomUser,
                        Genres = new List<GameCategory> { genres.Random() }
                    };
                    gameRepository.Add(game);
                }
            }

        }

        private static void SeedGameCategory(IServiceScope scope)
        {
            var defaultGenres = new List<string> { "Action", "Fight", "RPG", "Horror" };

            var gameCategoryRepository = scope.ServiceProvider
                .GetRequiredService<IGameCategoryRepository>();

            foreach (var genreName in defaultGenres)
            {
                if (gameCategoryRepository.Get(genreName) == null)
                {
                    var gameCatalog = new GameCategory
                    {
                        Name = genreName
                    };
                    gameCategoryRepository.Add(gameCatalog);
                }
            }
        }
    }
}