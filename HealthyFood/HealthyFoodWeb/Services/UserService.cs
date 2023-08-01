using Data.Interface.Models;
using Data.Interface.Repositories;
using HealthyFoodWeb.Models;
using HealthyFoodWeb.Services.IServices;

namespace HealthyFoodWeb.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(UserViewModel viewModel)
        {
            var User = new User
            {
                Name = viewModel.Name,
                AvatarUrl = viewModel.AvatarUrl,
                Password = viewModel.Password,
                Role = MyRole.User
            };
            _userRepository.Add(User);
        }

        public User GetById(int currentUserId)
        {
            return _userRepository.Get(currentUserId);
        }

        public List<User> GetUserModels()
        {
            return _userRepository
                .GetAll()
                .Where(x => x.AvatarUrl != null)
                .ToList();
        }

        public User Login(string login, string password)
        {
            return _userRepository.GetByNameAndPassword(login, password);
        }

        public void UploadUsers(IFormFile users)
        {
            var tempDirectory = Path.GetTempPath();
            var tempName = Path.GetTempFileName();
            var tempPath = Path.Combine(tempDirectory, tempName);
            using (var fs = File.Create(tempPath))
            {
                users.CopyTo(fs);
            }

            using(var fs = File.OpenRead(tempPath))
            {
                using (var sr = new StreamReader(fs))
                {
                    while(!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var data = line.Split(',');
                        var userName = data[0].Trim();
                        var password = data[1].Trim();

                        var user = new User
                        {
                            Name = userName,
                            Password = password,
                            AvatarUrl = "NoAvatar",
                            Role = MyRole.User
                        };

                        _userRepository.Add(user);
                    }
                }
            }
        }
    }
}
