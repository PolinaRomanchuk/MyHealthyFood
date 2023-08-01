namespace Data.Interface.Models
{
    public class StoreItem : BaseModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
