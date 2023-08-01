using Data.Interface.DataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(WebContext webContext) : base(webContext)
        {
        }

        public GamesCountData GetDataForGamesCount(int budget)
        {
            var availableGames = _dbSet.Where(x => x.Price <= budget);
            var count = availableGames.Count();
            var names = availableGames
                .OrderBy(x => x.Genres.Count)
                .Take(3)
                .Select(x => x.Name)
                .ToList();

            return new GamesCountData
            {
                Count = count,
                TopNames = names
            };
        }

        public Game GetGameAndGenres(int id)
        {
            return _dbSet
                .Include(x => x.Genres)
                .SingleOrDefault(x => x.Id == id);
        }

        public Game GetGameByName(string name)
        {
            return _dbSet.FirstOrDefault(x => x.Name == name);
        }

        public List<Game> GetGamesByUserId(int userId)
        {
            return _dbSet.Where(x => x.Creater.Id == userId).ToList();
        }

        public Game GetTheRichGameWithGenres()
        {
            return _dbSet
                .Include(x => x.Genres)
                .OrderByDescending(x => x.Price)
                .First();
        }

        public void RemoveByName(string name)
        {
            var game = GetGameByName(name);
            Remove(game.Id);
        }

        public void UpdateNameAndCover(int id, string name, string coverUrl)
        {
            var game = Get(id);
            game.Name = name;
            game.CoverUrl = coverUrl;
            _webContext.SaveChanges();
        }

        public override PaginatorData<Game> GetPaginator(int page, int perPage)
        {
            var initialSource = _dbSet.Include(x => x.Genres);
            return base.GetPaginator(initialSource, page, perPage);
        }
    }
}
