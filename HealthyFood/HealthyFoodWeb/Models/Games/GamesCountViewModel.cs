namespace HealthyFoodWeb.Models.Games
{
    public class GamesCountViewModel
    {
        public int TotalGamesCount { get; set; }
        public List<string> RandomGamesNames { get; set; } = new List<string>(); 
    }
}
