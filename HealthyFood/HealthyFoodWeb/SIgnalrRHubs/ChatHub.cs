using HealthyFoodWeb.Services.IServices;
using Microsoft.AspNetCore.SignalR;

namespace HealthyFoodWeb.SIgnalrRHubs
{
    public class ChatHub : Hub
    {
        private IAuthService _authService;

        public ChatHub(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task AddNewMessage(string message)
        {
            var user = _authService.GetUser();
            var username = user?.Name ?? "guest";
            await Clients.All.SendAsync("SomeOneAddNewMessage", message, username);
        }

        public async Task NewUserOpenChat()
        {
            var user = _authService.GetUser();
            var username = user?.Name ?? "guest";
            await Clients.All.SendAsync("SayHiToNewUser", username);
        }
    }
}
