namespace HealthyFoodWeb.Models
{
    public class ProductCountViewModel
    {
        public int TotalProductCount { get; set; }
        public List<string> ProductNames { get; set; } = new List<string>();
    }
}
