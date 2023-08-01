using Data.Interface.DataModels;
using Data.Interface.Models;
using HealthyFoodWeb.Models;
using HealthyFoodWeb.Models.Games;

namespace HealthyFoodWeb.Services.IServices
{
    public interface IGameService
    {
        Game GetTheBestGameWithGenres();

        string GetTheBestGameName();

        void CreateGame(GameViewModel viewModel);

        List<Game> GetAllCheapGames();

        List<Game> GetAllRichGames();

        void Remove(string name);

        PagginatorViewModel<GameViewModel> GetGamesForPaginator(int page, int perPage);
        
        GameViewModel GetGameViewModel(int id);
        
        void UpdateNameAndCover(int id, string name, string coverUrl);
        void UpdateGenres(int id, List<string> genres);

        GamesCountViewModel GetViewModelForGamesCount(int budget);
    }
}