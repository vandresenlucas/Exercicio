using Questao5.Application.Commands.Requests;

namespace Questao5.Domain.Entities.Movimento
{
    public interface IMovimentoCommandStore
    {
        Task RegistrarMovimentoAsync(Movimento movimento);
    }
}
