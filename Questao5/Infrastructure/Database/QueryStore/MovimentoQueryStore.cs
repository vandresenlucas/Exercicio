using Dapper;
using Questao5.Domain.Entities.Movimento;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using System.Data;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class MovimentoQueryStore : IMovimentoQueryStore
    {
        private readonly IDbConnection _dbConnection;

        public MovimentoQueryStore(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<BuscarMovimentosPorCcIdResponse> BuscarMovimentosPorContaCorrente(BuscarMovimentosPorCcIdRequest request)
        {
            var query = "SELECT valor, tipomovimento FROM movimento WHERE idcontacorrente = @Id";

            return await _dbConnection.QueryFirstOrDefaultAsync<BuscarMovimentosPorCcIdResponse>(query, new { Id = request.IdContaCorrente });
        }

        public async Task<CalcularSaldoContaCorrenteResponse> CalcularSaldoContaCorrente(CalcularSaldoContaCorrenteRequest request)
        {
            var query = @"SELECT c.numero NumeroCc,
	                             c.nome TitularCc,
                                 time('now', 'localtime') DataHora,
                                 Round(
	                                SUM(CASE WHEN m.tipomovimento = 'C' THEN m.valor ELSE 0 END) -
                                    SUM(CASE WHEN m.tipomovimento = 'D' THEN m.valor ELSE 0 END), 2
                                 ) AS Saldo
                            FROM movimento m
                            JOIN contacorrente c on m.idcontacorrente = c.idcontacorrente
                           WHERE c.idcontacorrente = @IdContaCorrente
                           GROUP BY c.numero, c.nome";

            return await _dbConnection.QueryFirstOrDefaultAsync<CalcularSaldoContaCorrenteResponse>(query, new { IdContaCorrente = request.IdContaCorrente });
        }
    }
}
