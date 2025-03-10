using Dapper;
using Questao5.Domain.Entities.ContaCorrente;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using System.Data;
using System.Text.Json;

namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class ContaCorrenteQueryStore : IContaCorrenteQueryStore
    {
        private readonly IDbConnection _dbConnection;

        public ContaCorrenteQueryStore(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ContaCorrenteResponse> BuscarContaCorrente(string ccId)
        {
            var result = await _dbConnection.QuerySingleOrDefaultAsync<string>(
                "SELECT * FROM idempotencia WHERE chave_idempotencia = @Id",
            new { Id = ccId });

            return result != null ? JsonSerializer.Deserialize<ContaCorrenteResponse>(result) : null;
        }
    }
}
