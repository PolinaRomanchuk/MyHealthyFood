using Data.Interface.DataModels;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        Game GetGameByName(string name);

        void RemoveByName(string name);

        Game GetTheRichGameWithGenres();

        List<Game> GetGamesByUserId(int userId);

        void UpdateNameAndCover(int id, string name, string coverUrl);

        Game GetGameAndGenres(int id);

        GamesCountData GetDataForGamesCount(int budget);
    }
}
