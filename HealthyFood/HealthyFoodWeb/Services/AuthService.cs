using Data.Interface.Models;
using HealthyFoodWeb.Services.IServices;

namespace HealthyFoodWeb.Services
{
    public class AuthService : IAuthService
    {
        public const string AUTH_NAME = "SmileCookciAuth";
        public const string AUTH_CLAIMS_ID_NAME = "Id";

        private IUserService _userService;
        private IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public User GetUser()
        {
            var userIdStr = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == AUTH_CLAIMS_ID_NAME)?.Value;
            if (userIdStr == null)
            {
                userIdStr = _httpContextAccessor.HttpContext.Request.Headers["Smile"];
            }

            var currentUserId = int.Parse(userIdStr ?? "0");
            var user = _userService.GetById(currentUserId);
            return user;
        }
    }
}
