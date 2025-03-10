using Dapper;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities.Idempotencia;
using Questao5.Infrastructure.Database.CommandStore.Responses;
using System.Data;
using System.Text.Json;

namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class IdempotenciaCommandStore : IIdempotenciaCommandStore
    {
        private readonly IDbConnection _dbConnection;

        public IdempotenciaCommandStore(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task RegistrarIdempotencia(string chaveIdempotencia, MovimentoCcCommand request, MovimentoCcResponse response)
        {
            var jsonRequest = JsonSerializer.Serialize(request);
            var jsonResponse = JsonSerializer.Serialize(response);

            await _dbConnection.ExecuteAsync("INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@Chave, @Requisicao, @Resultado)",
                new { Chave = chaveIdempotencia, Requisicao = jsonRequest, Resultado = jsonResponse });
        }
    }
}
