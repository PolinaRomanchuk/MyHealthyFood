using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Sql
{
    public class Startup
    {
        public void RegisterDbContext(IServiceCollection services)
        {
            var connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=HealthyFood;Trusted_Connection=True;";
            services.AddDbContext<WebContext>(op => op.UseSqlServer(connectionString));
        }
    }
}
