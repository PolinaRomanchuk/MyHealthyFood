namespace Data.Interface.Models
{
    public class Cart : BaseModel
    {
        private int quantity = 1;
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public virtual User Customer { get; set; }
        public virtual List<CartTags> Tags { get; set; }
        public int Quantity { get => quantity; set => quantity = value; }
    }
}
