using Dapper;
using Questao5.Domain.Entities.Idempotencia;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using System.Data;
using System.Text.Json;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class IdempotenciaQueryStore : IIdempotenciaQueryStore
    {
        private readonly IDbConnection _dbConnection;

        public IdempotenciaQueryStore(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IdempotenciaResponse?> BuscarIdempotenciaAsync(BuscarIdempotenciaRequest request)
        {
            var query = "SELECT resultado FROM idempotencia WHERE chave_idempotencia = @Chave";
            var result = await _dbConnection.QueryFirstOrDefaultAsync<string>(query, new { Chave = request.ChaveIdempotencia });

            return result != null ? JsonSerializer.Deserialize<IdempotenciaResponse>(result) : null;
        }
    }
}
