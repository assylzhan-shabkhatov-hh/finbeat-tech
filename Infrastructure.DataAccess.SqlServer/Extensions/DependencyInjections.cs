using Microsoft.EntityFrameworkCore;

namespace FinBetApi.Infrastructure.DataAccess.SqlServer.Extensions
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddFinbetDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FinBetDbContext>(opt =>
            {
                opt.UseInMemoryDatabase(configuration.GetConnectionString("FB"));
            });

            return services;
        }
    }
}
