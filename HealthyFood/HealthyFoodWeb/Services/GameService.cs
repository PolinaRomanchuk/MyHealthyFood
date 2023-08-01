using Data.Interface.DataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;
using HealthyFoodWeb.Models;
using HealthyFoodWeb.Models.Games;
using HealthyFoodWeb.Services.Helpers;
using HealthyFoodWeb.Services.IServices;

namespace HealthyFoodWeb.Services
{
    public class GameService : IGameService
    {
        public const decimal CHEAP_GAME_BORDER = 5;
        private IGameRepository _gameRepository;
        private IGameCategoryRepository _gameCategoryRepository;
        private IAuthService _authService;
        private IPagginatorService _pagginatorService;
        private IWebHostEnvironment _webHostEnvironment;

        public GameService(IGameRepository gameRepository,
            IAuthService authService,
            IGameCategoryRepository gameCategoryRepository,
            IPagginatorService pagginatorService,
            IWebHostEnvironment webHostEnvironment)
        {
            _gameRepository = gameRepository;
            _authService = authService;
            _gameCategoryRepository = gameCategoryRepository;
            _pagginatorService = pagginatorService;
            _webHostEnvironment = webHostEnvironment;
        }

        public void CreateGame(GameViewModel viewModel)
        {
            var user = _authService.GetUser();
            var dbGameModel = new Game()
            {
                Name = viewModel.Name,
                Price = viewModel.Price,
                CoverUrl = "TEMP",
                Creater = user
            };

            _gameRepository.Add(dbGameModel);
            
            var ext = Path.GetExtension(viewModel.CoverFile.FileName);
            var fileName = $"game-{dbGameModel.Id}{ext}";
            var path = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "images",
                "games",
                fileName);

            using (var fs = File.Create(path))
            {
                viewModel.CoverFile.CopyTo(fs);
            }

            dbGameModel.CoverUrl = $"/images/games/{fileName}";
            _gameRepository.Update(dbGameModel);

        }

        public Game GetTheBestGameWithGenres()
        {
            return _gameRepository.GetTheRichGameWithGenres();
        }

        public List<Game> GetAllCheapGames()
        {
            return _gameRepository
                .GetAll()
                .Where(x => x.Price < CHEAP_GAME_BORDER)
                .ToList();
        }

        public List<Game> GetAllRichGames()
        {
            return _gameRepository
                 .GetAll()
                 .Where(x => x.Price >= CHEAP_GAME_BORDER)
                 .ToList();
        }

        public string GetTheBestGameName()
        {
            //do some diff logic

            return _gameRepository.GetAll().First().Name;
        }

        public void Remove(string name)
        {
            _gameRepository.RemoveByName(name);
        }

        public PagginatorViewModel<GameViewModel> GetGamesForPaginator(int page, int perPage)
        {
            var viewModel = _pagginatorService
                .GetPaginatorViewModel(
                    page, 
                    perPage,
                    BuildViewModelFromDbModel,
                    _gameRepository);
            
            return viewModel;
        }

        public GameViewModel GetGameViewModel(int id)
        {
            var gameDb = _gameRepository.GetGameAndGenres(id);
            var genres = _gameCategoryRepository
                .GetAll()
                .Select(x => x.Name)
                .ToList();

            return new GameViewModel
            {
                Id = gameDb.Id,
                Name = gameDb.Name,
                CoverUrl = gameDb.CoverUrl,
                Price = gameDb.Price,
                AvailableGenres = genres,
                Genres = gameDb.Genres.Select(x => x.Name).ToList()
            };
        }

        public void UpdateNameAndCover(int id, string name, string coverUrl)
        {
            _gameRepository.UpdateNameAndCover(id, name, coverUrl);
        }

        public void UpdateGenres(int id, List<string> newGenresNames)
        {
            var game = _gameRepository.GetGameAndGenres(id);
            if (game.Genres == null)
            {
                game.Genres = new List<GameCategory>();
            }

            var newGenres = _gameCategoryRepository
                .GetAll()
                .Where(genre => newGenresNames.Contains(genre.Name))
                .ToList();

            game.Genres.RemoveAll(x => true);

            newGenres.ForEach(genre => game.Genres.Add(genre));
            // newGenres.ForEach(game.Genres.Add);

            _gameRepository.Update(game);
        }

        public GamesCountViewModel GetViewModelForGamesCount(int budget)
        {
            var dataModel = _gameRepository.GetDataForGamesCount(budget);
            return new GamesCountViewModel
            {
                TotalGamesCount = dataModel.Count,
                RandomGamesNames = dataModel.TopNames
            };
        }

        private GameViewModel BuildViewModelFromDbModel(Game x)
        {
            return new GameViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CoverUrl = x.CoverUrl,
                Genres = x.Genres?.Select(x => x.Name).ToList() ?? new List<string>()
            };
        }
    }
}
