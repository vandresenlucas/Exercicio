using Dapper;
using Questao5.Domain.Entities.ContaCorrente;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using System.Data;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class ContaCorrenteQueryStore : IContaCorrenteQueryStore
    {
        private readonly IDbConnection _dbConnection;

        public ContaCorrenteQueryStore(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<BuscarContaCorrenteResponse> BuscarContaCorrenteAsync(BuscarContaCorrenteRequest request)
        {
            var query = "SELECT * FROM contacorrente WHERE idcontacorrente = @Id";
 
            return await _dbConnection.QueryFirstOrDefaultAsync<BuscarContaCorrenteResponse>(query, new { Id = request.IdContaCorrente });
        }
    }
}
