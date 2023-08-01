namespace HealthyFoodWeb.Models
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public List<string> Products { get; set; } = new List<string>();

    }
}
