namespace Data.Interface.Models
{
    public class Game : BaseModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string CoverUrl { get; set; }

       
        public virtual User Creater { get; set; }

        public virtual List<GameCategory> Genres { get; set; }

        public virtual List<GameCategory> SecondaryGenres { get; set; }
    }
}
