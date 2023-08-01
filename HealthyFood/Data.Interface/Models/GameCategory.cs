namespace Data.Interface.Models
{
    public class GameCategory : BaseModel
    {
        public string Name { get; set; }

        public virtual List<Game> Games { get; set; }

        public virtual List<Game> SecondaryGames { get; set; }
    }
}
