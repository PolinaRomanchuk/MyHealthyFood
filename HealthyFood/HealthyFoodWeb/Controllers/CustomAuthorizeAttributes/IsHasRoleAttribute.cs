using Data.Interface.Models;
using HealthyFoodWeb.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HealthyFoodWeb.Controllers.CustomAuthorizeAttributes
{
    public class IsHasRoleAttribute : ActionFilterAttribute
    {
        private MyRole[] _roles;
        public IsHasRoleAttribute(params MyRole[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authService = context.HttpContext.RequestServices.GetService<IAuthService>();
            var user = authService.GetUser();

            if (!_roles.Contains(user.Role))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
