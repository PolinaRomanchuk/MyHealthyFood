using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class SimilarGameRepository : BaseRepository<SimilarGame>, ISimilarGameRepository
    {
        public SimilarGameRepository(WebContext webContext) : base(webContext)
        {
        }

        public void Remove(string name)
        {
            var gameForRemove = _webContext.SimilarGames.FirstOrDefault(x => x.Name == name);
            _webContext.SimilarGames.Remove(gameForRemove);
            _webContext.SaveChanges();
        }
    }
}
