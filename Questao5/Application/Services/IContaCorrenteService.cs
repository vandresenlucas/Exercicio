using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Services
{
    public interface IContaCorrenteService
    {
        Task<Result> ValidarContaCorrente(string idContaCorrente);
    }
}
