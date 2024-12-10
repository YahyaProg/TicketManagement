using Microsoft.Extensions.DependencyInjection;
using TanvirArjel.EFCore.GenericRepository;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)//, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // CORE
            services.AddGenericRepository<DBContext>();
            services.AddQueryRepository<DBContext>();

            return services;
        }
    }
}
