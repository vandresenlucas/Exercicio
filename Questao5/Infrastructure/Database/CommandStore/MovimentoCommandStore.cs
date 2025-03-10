using Dapper;
using Questao5.Domain.Entities.Movimento;
using System.Data;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class MovimentoCommandStore : IMovimentoCommandStore
    {
        private readonly IDbConnection _dbConnection;

        public MovimentoCommandStore(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task RegistrarMovimento(Movimento movimento)
        {
            var query = @"INSERT INTO movimento (datamovimento, valor, idmovimento, tipomovimento, idcontacorrente) 
                VALUES (@DataMovimento, @Valor, @IdMovimento, @TipoMovimento, @IdContaCorrente)";

            await _dbConnection.ExecuteAsync(query, movimento);
        }
    }
}
