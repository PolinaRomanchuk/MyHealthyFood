using Data.Interface.Models;
using Data.Sql;
using HealthyFoodWeb.Models;
using HealthyFoodWeb.Services;
using HealthyFoodWeb.Services.IServices;
using HealthyFoodWeb.SIgnalrRHubs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace HealthyFoodWeb.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;

        private IHubContext<AlertHub> _hubAlertContext;

        public UserController(IUserService userService, 
            IHubContext<AlertHub> hubContext)
        {
            _userService = userService;
            _hubAlertContext = hubContext;
        }

        public IActionResult Index()
        {
            var viewModels = _userService
                .GetUserModels()
                .Select(dbModel =>
                    new UserViewModel
                    {
                        Name = dbModel.Name,
                        AvatarUrl = dbModel.AvatarUrl,
                    })
                .ToList();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(UserViewModel viewModel)
        {
            _userService.AddUser(viewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = _userService.Login(viewModel.Login, viewModel.Password);

            if (user == null)
            {
                ModelState.AddModelError(nameof(viewModel.Login), "Wrong login. Or password. Or both");

                return View(viewModel);
            }

            var claims = new List<Claim>() {
                    new Claim(AuthService.AUTH_CLAIMS_ID_NAME, user.Id.ToString()),
                    new Claim("Name", user.Name),
                    new Claim(ClaimTypes.AuthenticationMethod, AuthService.AUTH_NAME)
                };

            var identity = new ClaimsIdentity(claims, AuthService.AUTH_NAME);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Upload(IFormFile users)
        {
            _userService.UploadUsers(users);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AlertAllUsers()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AlertAllUsers(string message)
        { 
            //_hubContext.Clients.All
            //    .SendAsync("ImportantEvent", message)
            //    .Wait();

            await _hubAlertContext.Clients.All
                .SendAsync("ImportantEvent", message);

            return View();
        }
    }
}
