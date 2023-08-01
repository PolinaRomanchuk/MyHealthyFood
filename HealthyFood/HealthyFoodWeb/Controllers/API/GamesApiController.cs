using HealthyFoodWeb.Models;
using HealthyFoodWeb.Models.Games;
using HealthyFoodWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HealthyFoodWeb.Controllers.API
{
    [ApiController]
    [Route("/api/game")]
    public class GamesApiController : Controller
    {
        private IGameService _gameService;

        public GamesApiController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [Route("GamesCount")]
        public GamesCountViewModel GetGamesCount(int budget)
        {
            var viewModel = _gameService.GetViewModelForGamesCount(budget);
            Thread.Sleep(5 * 1000);
            return viewModel;
        }

        [Route("GetGames")]
        public List<GameViewModel> GetGames(int page, int perPage)
        {
            var viewModels = _gameService.GetGamesForPaginator(page, perPage).Items;
            return viewModels;
        }
    }
}
