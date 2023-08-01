using Data.Interface.Models;
using HealthyFoodWeb.Models;

namespace HealthyFoodWeb.Services.IServices
{
    public interface IUserService
    {
        void AddUser(UserViewModel viewModel);
        User GetById(int currentUserId);
        List<User> GetUserModels();
        User Login(string login, string password);
        void UploadUsers(IFormFile users);
    }
}