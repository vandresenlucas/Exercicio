using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;
using System.Data;

namespace Questao5.Infrastructure.Providers
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new DatabaseConfig { Name = configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
            services.AddScoped<IDbConnection>(sp =>
            {
                var connectionString = configuration.GetConnectionString("DatabaseName");
                return new SqliteConnection(connectionString);
            });

            services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

            return services;
        }
    }
}
