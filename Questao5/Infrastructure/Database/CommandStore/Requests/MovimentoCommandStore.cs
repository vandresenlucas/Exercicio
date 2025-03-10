using Dapper;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities.Movimento;
using Questao5.Infrastructure.Database.CommandStore.Responses;
using System.Data;

namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class MovimentoCommandStore : IMovimentoCommandStore
    {
        private readonly IDbConnection _dbConnection;

        public MovimentoCommandStore(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<MovimentoResponse> RegistrarMovimento(MovimentoCcCommand movimentoCommand)
        {
            var idMovimento = Guid.NewGuid().ToString();
            var dataMovimento = DateTime.Now;

            await _dbConnection.ExecuteAsync(
                "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) " +
                "VALUES (@Id, @IdConta, @DataMovimento, @Tipo, @Valor)",
                new {
                    Id = idMovimento,
                    IdConta = movimentoCommand.IdContaCorrente,
                    Tipo = movimentoCommand.TipoMovimento,
                    DataMovimento = dataMovimento,
                    Valor = movimentoCommand.Valor
                });

            return new MovimentoResponse
            {
                IdMovimento = idMovimento,
                IdContaCorrente = movimentoCommand.IdContaCorrente,
                TipoMovimento = movimentoCommand.TipoMovimento,
                Valor = movimentoCommand.Valor,
                DataMovimento = dataMovimento
            };
        }
    }
}
