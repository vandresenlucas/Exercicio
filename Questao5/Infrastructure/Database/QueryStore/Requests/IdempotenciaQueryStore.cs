using Dapper;
using Questao5.Domain.Entities.Idempotencia;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using System.Data;
using System.Text.Json;

namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class IdempotenciaQueryStore : IIdempotenciaQueryStore
    {
        private readonly IDbConnection _dbConnection;

        public IdempotenciaQueryStore(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IdempotenciaResponse?> BuscarIdempotencia(string chaveIdempotencia)
        {
            var result = await _dbConnection.QuerySingleOrDefaultAsync<string>(
                "SELECT resultado FROM idempotencia WHERE chave_idempotencia = @Chave",
            new { Chave = chaveIdempotencia });

            return result != null ? JsonSerializer.Deserialize<IdempotenciaResponse>(result) : null;
        }
    }
}
