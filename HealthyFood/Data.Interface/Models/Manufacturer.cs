namespace Data.Interface.Models
{
    public class Manufacturer : BaseModel
    {
        public string Name { get; set; }
        public virtual List<StoreItem> StoreItems { get; set; }
    }
}
