using CustomerManager.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManager.Api.Extensions
{
    public static class DependencyInjection
    {

        public static void InjectDependencies(this IServiceCollection services, IConfiguration config)
        {
            //inject dbcontext
            var connectionString = config.GetConnectionString("postgres");
            services.AddDbContext<CustomerManagerContext>(options => options.UseNpgsql(connectionString));
        }
    }
}
