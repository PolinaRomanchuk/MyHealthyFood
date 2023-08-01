using Data.Interface.Models;
using HealthyFoodWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HealthyFoodWeb.Controllers.CustomAuthorizeAttributes
{
    public class IsAdminAttribute : ActionFilterAttribute
    {
        public IsAdminAttribute() { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authService = context.HttpContext.RequestServices.GetService<IAuthService>();
            var user = authService.GetUser();

            if (user.Role != MyRole.Admin)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
