using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Domain.Entities.Idempotencia
{
    public interface IIdempotenciaQueryStore
    {
        Task<IdempotenciaResponse> BuscarIdempotencia(string chaveIdempotencia);
    }
}
