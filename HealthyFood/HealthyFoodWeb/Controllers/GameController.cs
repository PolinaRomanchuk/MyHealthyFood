using Data.Interface.Models;
using HealthyFoodWeb.Controllers.CustomAuthorizeAttributes;
using HealthyFoodWeb.Models;
using HealthyFoodWeb.Models.Games;
using HealthyFoodWeb.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HealthyFoodWeb.Controllers
{

    public class GameController : Controller
    {
        private IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        } 

        [Authorize]
        public IActionResult Index()
        {
            var viewModel = new GameIndexViewModel();

            viewModel.CheapGames = _gameService
                .GetAllCheapGames()
                .Select(BuildViewModelFromDbModel)
                .ToList();
            
            viewModel.RichGames = _gameService
                .GetAllRichGames()
                .Select(BuildViewModelFromDbModel)
                .ToList();

            viewModel.TheBestGame = BuildViewModelFromDbModel(_gameService.GetTheBestGameWithGenres());

            return View(viewModel);
        }
        
        public IActionResult Games(int page = 1, int perPage = 10)
        {
            var viewModel = _gameService.GetGamesForPaginator(page, perPage);
            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        [IsHasRole(MyRole.Admin, MyRole.Manager)]
        public IActionResult CreateGame()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [IsHasRole(MyRole.Admin)]
        public IActionResult CreateGame(GameViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            _gameService.CreateGame(viewModel);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(string name)
        {
            _gameService.Remove(name);
            return RedirectToAction("Index");
        }

        public IActionResult RecomendateGame()
        {
            var bestGameName = _gameService.GetTheBestGameName();
            var recomendateGameViewModel = new GameViewModel
            {
                Name = bestGameName,
            };
            return View(recomendateGameViewModel);
        }

        public IActionResult Update(int id)
        {
            var viewModel = _gameService.GetGameViewModel(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(GameViewModel gameViewModel)
        {
            _gameService.UpdateNameAndCover(gameViewModel.Id,
                gameViewModel.Name,
                gameViewModel.CoverUrl);

            _gameService.UpdateGenres(gameViewModel.Id,
                gameViewModel.Genres);

            return RedirectToAction("Games", "Game");
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
