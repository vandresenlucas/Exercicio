using Questao5.Application.Services;
using Questao5.Domain.Entities.ContaCorrente;
using Questao5.Domain.Entities.Idempotencia;
using Questao5.Domain.Entities.Movimento;
using Questao5.Infrastructure.Database.CommandStore;
using Questao5.Infrastructure.Database.QueryStore;

namespace Questao5.Infrastructure.Providers
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IIdempotenciaCommandStore, IdempotenciaCommandStore>();
            services.AddScoped<IIdempotenciaQueryStore, IdempotenciaQueryStore>();
            services.AddScoped<IContaCorrenteQueryStore, ContaCorrenteQueryStore>();
            services.AddScoped<IMovimentoCommandStore, MovimentoCommandStore>();
            services.AddScoped<IMovimentoQueryStore, MovimentoQueryStore>();
            services.AddScoped<IContaCorrenteService, ContaCorrenteService>();

            return services;
        }
    }
}
