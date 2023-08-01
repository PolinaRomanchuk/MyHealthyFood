namespace Data.Interface.Models
{
    public class CartTags : BaseModel
    {
        public string Name { get; set; }
        public virtual List<Cart> CartItems { get; set; }
    }
}
