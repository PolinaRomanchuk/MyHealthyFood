namespace Data.Interface.Models
{
    public class Order : BaseModel
    {
        public DateTime OrderTime { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string? Comment { get; set; }
        public string PaymentMethod { get; set; }
        public virtual List <Cart> Products { get; set; }
    }
}
