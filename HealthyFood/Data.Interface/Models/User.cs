namespace Data.Interface.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; }

        public string AvatarUrl { get; set; }

        public string Password { get; set; }

        public MyRole Role { get; set; }

        public virtual List<Game> CreatedGames { get; set; }

        public virtual List<Cart> Products { get; set; }

        public virtual List<StoreItem> StoreItems { get; set; }
	}
}
