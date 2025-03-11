using Questao5.Infrastructure.Providers;

namespace Questao5.Infrastructure
{
    public static class ServiceCollection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDatabase(configuration);
            services.ConfigureServices();

            return services;
        }
    }
}
