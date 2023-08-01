namespace HealthyFoodWeb.Models
{
    public class PagginatorViewModel<T> where T : class
    {
        public List<T> Items { get; set; } = new List<T>();
        public int ActivePageNumber { get; set; }
        public List<int> PageList { get; set; }
    }
}
