namespace HealthyFoodWeb.Models
{
    public class GameIndexViewModel
    {
        public List<GameViewModel> CheapGames { get; set; }

        public List<GameViewModel> RichGames { get; set; }

        public GameViewModel TheBestGame {get;set;}
    }
}
