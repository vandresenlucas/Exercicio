using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Domain.Entities.Idempotencia
{
    public interface IIdempotenciaCommandStore
    {
        Task RegistrarIdempotencia(string chaveIdempotencia, MovimentoCcCommand request, MovimentoCcResponse response);
    }
}
