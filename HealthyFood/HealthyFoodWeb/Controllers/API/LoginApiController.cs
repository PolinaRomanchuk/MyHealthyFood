using Data.Interface.Models;
using HealthyFoodWeb.Models;
using HealthyFoodWeb.Models.Games;
using HealthyFoodWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HealthyFoodWeb.Controllers.API
{
    [ApiController]
    [Route("/api/login")]
    public class LoginApiController : Controller
    {
        private IUserService _userService;

        public LoginApiController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("GetUserId")]
        public int GetUserId(string login, string password)
        {
            var viewModel = _userService.Login(login, password);

            return viewModel.Id;
        }

        [Route("GetUser")]
        public User GetUser(int id)
        {
            var viewModel = _userService.GetById(id);

            return viewModel;
        }
    }
}
