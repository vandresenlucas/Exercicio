using Questao5.Application.Commands.Requests;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Domain.Entities.Movimento
{
    public interface IMovimentoCommandStore
    {
        Task<MovimentoResponse> RegistrarMovimento(MovimentoCcCommand movimentoCommand);
    }
}
