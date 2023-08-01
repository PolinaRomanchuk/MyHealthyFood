using Microsoft.Data.SqlClient;

namespace HealthyFoodWeb.Services.Middlewares
{
    public class CheckUpdateDatabaseMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckUpdateDatabaseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (SqlException e)
            {
                // We expect that developer forgot update Database 
                context.Response.Redirect("/Home/RemindUpdateDatabase");
            }

        }
    }
}
