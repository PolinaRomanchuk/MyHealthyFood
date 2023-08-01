using HealthyFoodWeb.Models;
using HealthyFoodWeb.Services;
using HealthyFoodWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HealthyFoodWeb.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;
        private IAuthService _authService;

        public HomeController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        public IActionResult Index()
        {
            var user = _authService.GetUser();

            var viewModel = new ProfileViewModel();

            viewModel.Name = user?.Name ?? "Гость";

            return View(viewModel);
        }

       
        public IActionResult Privacy()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new Exception("I don't want to work on Sunday");
            }
            return View();
        }

        public IActionResult RemindUpdateDatabase()
        {
            return View();
        }
    }
}