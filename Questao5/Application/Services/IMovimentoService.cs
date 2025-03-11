using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Services
{
    public interface IMovimentoService
    {
        Task<Result> ValidarMovimento(string idContaCorrente);
    }
}
